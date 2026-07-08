
vs.1.1

;------------------------------------------------------------------------------
; v0 = position
; v1 = blend weights
; v2 = blend indices
; v3 = normal
; v4 = texture coordinates
;------------------------------------------------------------------------------

;------------------------------------------------------------------------------
; r0.w = Last blend weight
; r1 = Blend indices
; r2 = Temp position
; r3 = Temp normal
; r4 = Blended position in camera space
; r5 = Blended normal in camera space
;------------------------------------------------------------------------------

;------------------------------------------------------------------------------
; Constants specified by the app;
;
; c36-c95 = world-view matrix palette
; c16	  = diffuse * light.diffuse
; c15	  = ambient color
; c0-c3   = world-view-projection matrix
; c32      = world matrix
; c13     = light direction
; c9	  = {1, power, 0, 1020.01};
;------------------------------------------------------------------------------

;------------------------------------------------------------------------------
; oPos	  = Output position
; oD0	  = Diffuse
; oD1	  = Specular
; oT0	  = texture coordinates
;------------------------------------------------------------------------------

dcl_position v0;
dcl_blendweight v1;
dcl_blendindices v2;
dcl_normal v3;
dcl_texcoord0 v4;

#define VSC_WORLD_VIEW_PROJECTION		0			// WVP
#define VSC_WORLD_VIEW					4			// WV
#define VSC_WORLD						8			// World
#define VSC_EYE_POSITION				12

#define	VSC_UTIL						13			// 1.0f, 100.0f, 0.0f, 765.01f
#define VSC_HALF						14

//#define VSC_ZERO						10
//#define VSC_ONE						12

#define VSC_SUN_DIRECTION				15			//(.w = intensity)		
#define VSC_SUN_COLOR					16
#define VSC_LIGHT_AMBIENT				17
#define VSC_LIGHT_DIFFUSE				18
#define VSC_BUMP_SCALE					19

#define VSC_TERRAIN_REFLECTANCE			20

#define VSC_BETA_1						21
#define VSC_BETA_2						22
#define VSC_BETA_DASH_1					23
#define VSC_BETA_DASH_2					24
#define VSC_BETA_1_PLUS_2				25
#define VSC_ONE_OVER_BETA_1_PLUS_2		26

#define VSC_HG							27		// = 1-g^2, 1+g, 2g, 0
#define VSC_CONSTANTS					28		// = 1.0, log_2 e, 0, 0
#define VSC_TERM_MULTIPLIERS			29		// = frac_ext, frac_ins.

#define VSC_CLOUD_TEX_PROJECTION_0		30
#define VSC_CLOUD_TEX_PROJECTION_1		31



#define VSC_COMMON						33


// Compensate for lack of UBYTE4 on Geforce3
mul r1,v2.zyxw,c[VSC_UTIL].wwww
;mul r1,v2,c[VSC_UTIL].wwww

//first compute the last blending weight
dp3 r0.w,v1.xyz,c[VSC_UTIL].xzz; 
add r0.w,-r0.w,c[VSC_UTIL].x

//Set 1
mov a0.x,r1.x
m4x3 r4.xyz,v0,c[a0.x + VSC_COMMON];
m3x3 r5.xyz,v3,c[a0.x + VSC_COMMON]; 

//blend them
mul r4.xyz,r4.xyz,v1.xxxx
mul r5.xyz,r5.xyz,v1.xxxx

//Set 2
mov a0.x, r1.y
m4x3 r2.xyz,v0,c[a0.x + VSC_COMMON];
m3x3 r3.xyz,v3,c[a0.x + VSC_COMMON];

//add them in
mad r4.xyz, r2.xyz, r0.wwww, r4;
mad r5.xyz, r3.xyz, r0.wwww, r5;

//compute position
mov r4.w, c[VSC_UTIL].x
m4x4 oPos, r4, c[VSC_WORLD_VIEW_PROJECTION]

// normalize normals
m3x3 r4, r5, c[VSC_WORLD]

;dp3 r4.w, r4, r4;
;rsq r4.w, r4.w;
;mul r4, r4, r4.w;

; Do the lighting calculation
//dp3 r1, r4, c[VSC_SUN_DIRECTION]			; normal dot light
//lit r1, r1
//mul r0, r1.y, c[VSC_LIGHT_DIFFUSE]		; Multiply with diffuse
//add r0, r0, c[VSC_LIGHT_AMBIENT]			; Add in ambient
//min oD0, r0, c[VSC_UTIL].x		; clamp if > 1

; Do the lighting calculation
dp3 r1.x, r4, c[VSC_SUN_DIRECTION]     ; normal dot light
mul r0, r1.x, c[VSC_LIGHT_DIFFUSE]      ; Multiply with diffuse
add r0, r0, c[VSC_LIGHT_AMBIENT]        ; Add in ambient
min oD0, r0, c[VSC_UTIL].x    ; clamp if > 1


; Copy texture coordinate
mov oT0, v4
