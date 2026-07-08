;#include "TerrainVSShaderConstants.h"
#define CV_WORLD					0
#define CV_WORLD_0					0
#define CV_WORLD_1					1
#define CV_WORLD_2					2
#define CV_WORLD_3					3

#define CV_WORLD_VIEW			4
#define CV_WORLD_VIEW_0			4
#define CV_WORLD_VIEW_1			5
#define CV_WORLD_VIEW_2			6
#define CV_WORLD_VIEW_3			7

#define CV_WORLD_VIEW_PROJECTION		8
#define CV_WORLD_VIEW_PROJECTION_0		8
#define CV_WORLD_VIEW_PROJECTION_1		9
#define CV_WORLD_VIEW_PROJECTION_2		10
#define CV_WORLD_VIEW_PROJECTION_3		11


#define CV_SUN_DIRECTION			12			//(.w = intensity)		
#define CV_SUN_COLOR				15
#define CV_TERRAIN_REFLECTANCE		13

#define CV_BETA_1					30
#define CV_BETA_2					29
#define CV_BETA_DASH_1				27
#define CV_BETA_DASH_2				26
#define CV_BETA_1_PLUS_2			24
#define CV_ONE_OVER_BETA_1_PLUS_2	23

#define CV_HG						28		// = 1-g^2, 1+g, 2g, 0
#define CV_CONSTANTS				31		// = 1.0, log_2 e, 0, 0
#define CV_TERM_MULTIPLIERS			25		// = frac_ext, frac_ins.

#define CV_ZERO						20
#define CV_ONE						21


//#define CV_BASE_TEX_PROJECTION			16
//#define CV_DETAIL_TEX_PROJECTION		17
#define CV_CLOUD_TEX_PROJECTION_0		18
#define CV_CLOUD_TEX_PROJECTION_1		19

#define CV_EYE_POSITION				22


#define VPOSITION								v0


; Scattering Vertex shader
; (c) 2002 Nathaniel Hoffman, Kenneth J. Mitchell and Arcot J. Preetham
;
; 

vs.1.1

dcl_position0 v0
dcl_texcoord0 v1
dcl_texcoord1 v2



; 
; V - View direction
; L - Sun direction
; Theta - Scattering angle
; s - Distance
; E - Total extinction (including reflectance).

; Transformation.
m4x4 oPos, VPOSITION, c[CV_WORLD_VIEW_PROJECTION]	; Transform position to viewport

; Calculate V
m4x4 r1, VPOSITION, c[CV_WORLD]	        ; Transform position to world space

mad oT2.xy, r1.xz, c[CV_CLOUD_TEX_PROJECTION_0].xy, c[CV_CLOUD_TEX_PROJECTION_0].zw

sub r1, c[CV_EYE_POSITION], r1	        ; V = eye - position
dp3 r1.w, r1, r1                        ; Normalize V.
rsq r1.w, r1.w							
mul r1, r1, r1.w
//mov oT3.xy, -r1.xy                     ; For sky, if applying texture.

; Texture Coordinates.
; 0 - Normal/Horizon map
; 1 - Terrain texture
; 2 - Cloud texture
mov oT0.xy, v1
mov oT1.xy, v2




; Angle (theta) between sun direction (L) and view direction (V).
dp3 r0.x, r1, c[CV_SUN_DIRECTION]       ; r0.x = [cos(theta)] = V.L
mad r0.y, r0.x, r0.x, c[CV_CONSTANTS].x	; r0.y = [1+cos^2(theta)] = Phase1(theta)


; Distance (s)
m4x4 r1, v0, c[CV_WORLD_VIEW]           ; r1.z = s
mov r0.z, r1.z							; store in r0.z for future use.

; Terms used in the scattering equation.
; r0 = [cos(theta), 1+cos^2(theta), s] 

; Extinction term E

mul r1, c[CV_BETA_1_PLUS_2], -r0.z       ; -(beta_1+beta_2) * s
mul r1, r1, c[CV_CONSTANTS].y           ; -(beta_1+beta_2) * s * log_2 e
exp r1.x, r1.x					
exp r1.y, r1.y					
exp r1.z, r1.z                          ; r1 = e^(-(beta_1 + beta_2) * s) = E1


; Apply Reflectance to E to get total net effective E
mul r3, r1, c[CV_TERRAIN_REFLECTANCE]   ;r3 = E (Total extinction) 


; Phase2(theta) = (1-g^2)/(1+g-2g*cos(theta))^(3/2)
; theta is 180 - actual theta (this corrects for sign)
; c[CV_HG] = [1-g^2, 1+g, 2g]
mad r4.x, c[CV_HG].z, r0.x, c[CV_HG].y; 


rsq r4.x, r4.x						
mul r4.y, r4.x, r4.x			
mul r4.x, r4.y, r4.x;
mul r0.w, r4.x, c[CV_HG].x              ; r0.w = Phase2(theta)


; Inscattering (I) = (Beta'_1 * Phase_1(theta) + Beta'_2 * Phase_2(theta)) * 
;        [1-exp(-Beta_1*s).exp(-Beta_2*s)] / (Beta_1 + Beta_2)

mul r4, c[CV_BETA_DASH_1], r0.y 
mul r5, c[CV_BETA_DASH_2], r0.w  
sub r6, c[CV_CONSTANTS].x, r1
mov r7, c[CV_BETA_1_PLUS_2]

add r4, r4, r5
mul r4, r4, r6
mul r4, r4, c[CV_ONE_OVER_BETA_1_PLUS_2]	; r4 = I (inscattering)


; Apply Inscattering contribution factors.

mul r4, r4, c[CV_TERM_MULTIPLIERS].y


; Scale with Sun color & intesity.
mul r4, r4, c[CV_SUN_COLOR]		
mul r4, r4, c[CV_SUN_COLOR].w	

mul r3, r3, c[CV_SUN_COLOR]		
mul r3, r3, c[CV_SUN_COLOR].w	

; Outputs.
mov oD0, r3                             ; Extinction
mov oD1, r4                             ; Inscattering
