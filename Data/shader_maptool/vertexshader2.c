
vs.1.1

;------------------------------------------------------------------------------
; v0 = position
; v1 = blend weights
; v2 = blend indices
; v3 = normal
; v4 = texture coordinates
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
#define VSC_WORLD						4			// World
#define	VSC_UTIL						8		// 1.0f, 100.0f, 0.0f, 765.01f
#define VSC_DLIGHT_DIRECTION			9
#define VSC_DLIGHT_AMBIENT				10
#define VSC_DLIGHT_DIFFUSE				11
#define VSC_PLIGHT_POSITION				12
#define VSC_PLIGHT_AMBIENT				13
#define VSC_PLIGHT_DIFFUSE				14
#define VSC_PLIGHT_ATTENUATION			15
#define VSC_FOG							16
#define VSC_ENV_TEXTURE_MATRIX			17          // 4°³
#define VSC_WORLDVIEW					21			// 4°³
#define	VSC_UTIL2						25		// 1.0f/127.0f, -1.0f, 1.0f/65535.0f, 0.5f
#define VSC_COMMON						26

def c[VSC_UTIL],  1.0f,  0.5f, 0.0f, 765.01f
def c[VSC_UTIL2], 2.0f, -1.0f, 0.000305f, 0.5f
	 

// Compensate for lack of UBYTE4 on Geforce3
mul r1.xy, v2.zy,c[VSC_UTIL].w

//first compute the last blending weight
dp3 r0.w, v1.xyz, c[VSC_UTIL].xzz;
add r0.w, -r0.w, c[VSC_UTIL].x

;;mad r7.xyz, v3.xyz, c[VSC_UTIL2].xxx, c[VSC_UTIL2].yyy

//Set 1
mov a0.x,r1.x
m4x3 r4.xyz,v0,c[a0.x + VSC_COMMON]
m3x3 r5.xyz,v3.xyz,c[a0.x + VSC_COMMON]

//blend them
mul r4.xyz,r4.xyz,v1.x
mul r5.xyz,r5.xyz,v1.x

//Set 2
mov a0.x, r1.y
m4x3 r2.xyz,v0,c[a0.x + VSC_COMMON];
m3x3 r3.xyz,v3.xyz,c[a0.x + VSC_COMMON];

//add them in
mad r4.xyz, r2.xyz, r0.w, r4;
mad r5.xyz, r3.xyz, r0.w, r5;

//compute position
mov r4.w, c[VSC_UTIL].x
m4x4 oPos, r4, c[VSC_WORLD_VIEW_PROJECTION]

// normalize normals
m3x3 r3.xyz, r5.xyz, c[VSC_WORLD]

dp3 r3.w, r3, r3;
rsq r3.w, r3.w;
mul r3, r3, r3.w;

; Do the lighting calculation(Directional)
dp3 r1.x, r3, c[VSC_DLIGHT_DIRECTION]		; normal dot light
max r1.x, r1.x, c[VSC_UTIL].z	

mov r6, c[VSC_DLIGHT_AMBIENT]
mad oD0, r1.x, c[VSC_DLIGHT_DIFFUSE], r6      ; Multiply with diffuse

;mov oD0, r7

; Copy texture coordinate
;mul oT0.xy, v4.xy, c[VSC_UTIL2].zz
mov oT0.xy, v4.xy

; Fog
m4x4 r5, r4, c[VSC_WORLDVIEW]
add r0.x, -r5.z, c[VSC_FOG].y
mul r0.x, r0.x, c[VSC_FOG].z
max r0.x, r0.x, c[VSC_UTIL].z       ; clamp fog to > 0.0
min oFog, r0.x, c[VSC_UTIL].x     ; clamp fog to < 1.0


