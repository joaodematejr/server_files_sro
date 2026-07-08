vs.1.1

dcl_position v0;
dcl_normal v3;
dcl_texcoord0 v4;

#define VSC_WORLD_VIEW_PROJECTION		0			// WVP
#define VSC_WORLDVIEW					4			// World
#define	VSC_UTIL						8		// 1.0f, 100.0f, 0.0f, 765.01f
#define VSC_DLIGHT_DIRECTION			9
#define VSC_DLIGHT_AMBIENT				10
#define VSC_DLIGHT_DIFFUSE				11
#define VSC_PLIGHT_POSITION				12
#define VSC_PLIGHT_AMBIENT				13
#define VSC_PLIGHT_DIFFUSE				14
#define VSC_PLIGHT_ATTENUATION			15
#define VSC_FOG							16
#define VSC_ENV_TEXTURE_MATRIX			17          // 4개
#define VSC_WORLD_POS					21			// 1개
#define VSC_HEIGHTFOG					22
#define VSC_TEXTUREANI_MATRIX			23
#define VSC_COMMON						26

def c[VSC_UTIL],  1.0f,  0.5f, 0.0f, 765.01f

// --------------------------------   No Weight

//compute position
m4x4 oPos,v0,c[VSC_WORLD_VIEW_PROJECTION]

// normalize normals
//m3x3 r3.xyz, v3.xyz, c[VSC_WORLD]

// --------------------------------   No Weight

; Do the lighting calculation(Directional)
dp3 r1.x, v3, c[VSC_DLIGHT_DIRECTION]		; normal dot light
max r1.x, r1.x, c[VSC_UTIL].z	

mov r6, c[VSC_DLIGHT_AMBIENT]
mad oD0, r1.x, c[VSC_DLIGHT_DIFFUSE], r6      ; Multiply with diffuse

; Copy texture coordinate
mov oT0.xy, v4.xy

// Fog
m4x4 r5, v0, c[VSC_WORLDVIEW]

mul r0.x, r5.z, c[VSC_FOG].w
mul r0.y, r0.x, r0.x
exp r0.z, r0.y
rcp r0.x, r0.z

//add r0.x, -r5.z, c[VSC_FOG].y
//mul r0.x, r0.x, c[VSC_FOG].z

add  r4.y, v0.y, c[VSC_WORLD_POS].y
sub r3.x, r4.y, c[VSC_HEIGHTFOG].x
max r3.x, r3.x, -r3.x  	// Center Y 로부터의 거리

add r3.y, r3.x, -c[VSC_HEIGHTFOG].y
mul r3.z, r3.y, c[VSC_HEIGHTFOG].w
mov r3.y, -c[VSC_HEIGHTFOG].z
mad r3.x, r3.y, r3.z, c[VSC_UTIL].x



min r0.x, r0.x, r3.x    //거리 FOG와 높이 FOG중 더 센것을 취한다

max r0.x, r0.x, c[VSC_UTIL].z       ; clamp fog to > 0.0
min oFog, r0.x, c[VSC_UTIL].x     ; clamp fog to < 1.0

