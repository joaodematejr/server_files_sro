
vs.1.1

dcl_position v0;
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



//compute position
m4x4 oPos,v0,c[VSC_WORLD_VIEW_PROJECTION]

// normalize normals
m3x3 r5,v3,c[VSC_WORLD];

//mov r5, v3
;dp3 r5.w, r5, r5;
;rsq r5.w, r5.w;
;mul r5, r5, r5.w;

; Do the lighting calculation
//dp3 r1, r5, c[VSC_SUN_DIRECTION]			; normal dot light
//lit r1, r1
//mul r0, r1.y, c[VSC_LIGHT_DIFFUSE]		; Multiply with diffuse
//add r0, r0, c[VSC_LIGHT_AMBIENT]			; Add in ambient
//min oD0, r0, c[VSC_UTIL].x		; clamp if > 1

; Do the lighting calculation
dp3 r1.x, r5, c[VSC_SUN_DIRECTION]     ; normal dot light
mul r0, r1.x, c[VSC_LIGHT_DIFFUSE]      ; Multiply with diffuse
add r0, r0, c[VSC_LIGHT_AMBIENT]        ; Add in ambient
min oD0, r0, c[VSC_UTIL].x    ; clamp if > 1

; Copy texture coordinate
mov oT0, v4
;mov oT1, v4
;mov oT2, v4



