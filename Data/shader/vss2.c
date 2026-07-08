/////////////////////////////////////////////////////// Version

#ifdef VS_VER_1_1
vs_1_1
#endif

#ifdef VS_VER_2_SW
vs_2_sw
#endif


/////////////////////////////////////////////////////// Declare

#ifdef VS_DCL_SKIN2
//------------------------------------------------------------------------------
// v0 = position
// v1 = blend weights
// v2 = blend indices
// v3 = normal
// v4 = texture coordinates
//------------------------------------------------------------------------------
//------------------------------------------------------------------------------
// oPos	  = Output position
// oD0	  = Diffuse
// oD1	  = Specular
// oT0	  = texture coordinates
//------------------------------------------------------------------------------

dcl_position v0
dcl_blendweight v1
dcl_blendindices v2
dcl_normal v3
dcl_texcoord0 v4
#endif


/////////////////////////////////////////////////////// Define

#ifdef VS_CONSTANT
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
#endif


/////////////////////////////////////////////////////// Position

#ifdef VS_POS_NORMAL_SKIN2
// Compensate for lack of UBYTE4 on Geforce3

mul r1.xy, v2.zy,c[VSC_UTIL].w

//first compute the last blending weight
dp3 r0.w, v1.xyz, c[VSC_UTIL].xzz
add r0.w, -r0.w, c[VSC_UTIL].x

//Set 1
mov a0.x,r1.x

m4x3 r4.xyz,v0,c[a0.x + VSC_COMMON]
m3x3 r5.xyz,v3.xyz,c[a0.x + VSC_COMMON]

//blend them
mul r4.xyz,r4.xyz,v1.x
mul r5.xyz,r5.xyz,v1.x

//Set 2
mov a0.x, r1.y
m4x3 r2.xyz,v0,c[a0.x + VSC_COMMON]
m3x3 r3.xyz,v3.xyz,c[a0.x + VSC_COMMON]

//add them in
mad r4.xyz, r2.xyz, r0.w, r4
mad r3.xyz, r3.xyz, r0.w, r5

//compute position
mov r4.w, c[VSC_UTIL].x
m4x4 oPos, r4, c[VSC_WORLD_VIEW_PROJECTION]


// normalize normals
//m3x3 r3.xyz, r5.xyz, c[VSC_WORLD]

dp3 r3.w, r3, r3
rsq r3.w, r3.w
mul r3, r3, r3.w

#endif



#ifdef VS_POS_NORMAL_SKIN2_V2.0
// Compensate for lack of UBYTE4 on Geforce3

mul r1.xy, v2.zy,c[VSC_UTIL].w

//first compute the last blending weight
dp3 r0.w, v1.xyz, c[VSC_UTIL].xzz
add r0.w, -r0.w, c[VSC_UTIL].x

//Set 1
mova a0.x,r1.x

m4x3 r4.xyz,v0,c[a0.x + VSC_COMMON]
m3x3 r5.xyz,v3.xyz,c[a0.x + VSC_COMMON]

//blend them
mul r4.xyz,r4.xyz,v1.x
mul r5.xyz,r5.xyz,v1.x

//Set 2
mova a0.x, r1.y
m4x3 r2.xyz,v0,c[a0.x + VSC_COMMON]
m3x3 r3.xyz,v3.xyz,c[a0.x + VSC_COMMON]

//add them in
mad r4.xyz, r2.xyz, r0.w, r4
mad r3.xyz, r3.xyz, r0.w, r5

//compute position
mov r4.w, c[VSC_UTIL].x
m4x4 oPos, r4, c[VSC_WORLD_VIEW_PROJECTION]


// normalize normals
//m3x3 r3.xyz, r5.xyz, c[VSC_WORLD]

dp3 r3.w, r3, r3
rsq r3.w, r3.w
mul r3, r3, r3.w

#endif


/////////////////////////////////////////////////////// Light


#ifdef VS_LIGHT_SKIN2_DIRECT
// Do the lighting calculation(Directional)
dp3 r1.x, r3, c[VSC_DLIGHT_DIRECTION]		// normal dot light
max r1.x, r1.x, c[VSC_UTIL].z	

mov r6, c[VSC_DLIGHT_AMBIENT]
mad oD0, r1.x, c[VSC_DLIGHT_DIFFUSE], r6      // Multiply with diffuse

#endif


#ifdef VS_LIGHT_SKIN2_NOLIGHT
// Do the lighting calculation(Directional)
mov oD0, c[VSC_UTIL].x
#endif


#ifdef VS_LIGHT_SKIN2_PLIGHT
// Do the lighting calculation(Directional)
dp3 r1.x, r3, c[VSC_DLIGHT_DIRECTION]		// normal dot light
max r1.x, r1.x, c[VSC_UTIL].z	

mov r6, c[VSC_DLIGHT_AMBIENT]
mad r6, r1.x, c[VSC_DLIGHT_DIFFUSE], r6      // Multiply with diffuse


// Do the lighting calculation(Point)
//Light와 버텍스 사이의 거리 계산
add r1, c[VSC_PLIGHT_POSITION], -r4
dp3 r5.w, r1, r1	// R_TEMP.w에 d*d
rsq r1.w, r5.w			// R_VERTEX_TO_LIGHT.w 에  1/d

//감쇠
dst r5, r5.wwww, r1.wwww          // R_TEMP = ( 1,    d, d*d, 1/d)
dp3 r5.w, r5, c[VSC_PLIGHT_ATTENUATION]		// R_TEMP.xyzw = (a0 + a1*d + a2*d*d)

rcp r5.w, r5.w                                 // R_TEMP2 = 1/R_TEMP.xyzw
mul r1, r1, r1.w //버텍스 방향으로 단위 라이트

dp3 r1.w, r3, r1  // R_DOT.x = 노멀하구 라이트 내적
max r1.w, r1.w, c[VSC_UTIL].z	
mul r1.w, r1.w, r5.w

//mul r2, r5.w, c[VSC_PLIGHT_AMBIENT]
mov r2, c[VSC_PLIGHT_AMBIENT]
mad r5, r1.w, c[VSC_PLIGHT_DIFFUSE], r2


// Do the lighting calculation(Directional + Point)
add oD0, r5, r6
#endif




/////////////////////////////////////////////////////// Texture UV


#ifdef VS_UV_SKIN2_TEXTURE0
// Copy texture coordinate
mov oT0.xy, v4.xy
#endif


#ifdef VS_UV_SKIN2_SPEC
m3x3 r0.xyz, r3.xyz, c[VSC_ENV_TEXTURE_MATRIX]
add oT0.xy, r0.xy, c[VSC_UTIL].yy

//m3x3 oT0.xyz, r3.xyz, c[VSC_ENV_TEXTURE_MATRIX]
mov oT1.xy, v4.xy

#endif


#ifdef VS_UV_SKIN2_MULTITEX
m4x4 r0.xyzw, v4.xyzw, c[VSC_ENV_TEXTURE_MATRIX]
mov oT0.xy, r0.xy

mov oT1.xy, v4.xy
#endif


#ifdef VS_UV_SKIN2_TEXANI0
mov r1.xy, v4.xy
mov r1.z, c[VSC_UTIL].x
m3x3 oT0.xyz, r1.xyz, c[VSC_TEXTUREANI_MATRIX]
#endif

#ifdef VS_UV_SKIN2_TEXANI1
mov oT0.xy, v4.xy

mov r1.xy, v4.xy
mov r1.z, c[VSC_UTIL].x
m3x3 oT1.xyz, r1.xyz, c[VSC_TEXTUREANI_MATRIX]
#endif

#ifdef VS_UV_SKIN2_TEXANI1R
mov r1.xy, v4.xy
mov r1.z, c[VSC_UTIL].x
m3x3 oT0.xyz, r1.xyz, c[VSC_TEXTUREANI_MATRIX]

mov oT1.xy, v4.xy
#endif


/////////////////////////////////////////////////////// Fog

#ifdef VS_FOG_SKIN2_LINEAR
// Fog
m4x4 r5, r4, c[VSC_WORLDVIEW]

add r0.x, -r5.z, c[VSC_FOG].y
mul r0.x, r0.x, c[VSC_FOG].z

max r0.x, r0.x, c[VSC_UTIL].z       // clamp fog to > 0.0
min oFog, r0.x, c[VSC_UTIL].x     // clamp fog to < 1.0
#endif


#ifdef VS_FOG_SKIN2_EXP
// Fog
m4x4 r5, r4, c[VSC_WORLDVIEW]

mul r0.x, r5.z, c[VSC_FOG].w
mul r0.y, r0.x, r0.x
exp r0.z, r0.y
rcp r0.x, r0.z

max r0.x, r0.x, c[VSC_UTIL].z       // clamp fog to > 0.0
min oFog, r0.x, c[VSC_UTIL].x     // clamp fog to < 1.0

#endif


#ifdef VS_FOG_SKIN2_LINEAR+HEIGHT
// Fog
m4x4 r5, r4, c[VSC_WORLDVIEW]

add r0.x, -r5.z, c[VSC_FOG].y
mul r0.x, r0.x, c[VSC_FOG].z

add  r4.y, v0.y, c[VSC_WORLD_POS].y
sub r3.x, r4.y, c[VSC_HEIGHTFOG].x
max r3.x, r3.x, -r3.x  	// Center Y 로부터의 거리

add r3.y, r3.x, -c[VSC_HEIGHTFOG].y
mul r3.z, r3.y, c[VSC_HEIGHTFOG].w
mov r3.y, -c[VSC_HEIGHTFOG].z
mad r3.x, r3.y, r3.z, c[VSC_UTIL].x

min r0.x, r0.x, r3.x    //거리 FOG와 높이 FOG중 더 센것을 취한다

max r0.x, r0.x, c[VSC_UTIL].z       // clamp fog to > 0.0
min oFog, r0.x, c[VSC_UTIL].x     // clamp fog to < 1.0
#endif



#ifdef VS_FOG_SKIN2_EXP+HEIGHT
// Fog
m4x4 r5, r4, c[VSC_WORLDVIEW]

mul r0.x, r5.z, c[VSC_FOG].w
mul r0.y, r0.x, r0.x
exp r0.z, r0.y
rcp r0.x, r0.z

add  r4.y, r4.y, c[VSC_WORLD_POS].y
sub r3.x, r4.y, c[VSC_HEIGHTFOG].x
max r3.x, r3.x, -r3.x  	// Center Y 로부터의 거리

add r3.y, r3.x, -c[VSC_HEIGHTFOG].y
mul r3.z, r3.y, c[VSC_HEIGHTFOG].w
mov r3.y, -c[VSC_HEIGHTFOG].z
mad r3.x, r3.y, r3.z, c[VSC_UTIL].x

min r0.x, r0.x, r3.x    //거리 FOG와 높이 FOG중 더 센것을 취한다

max r0.x, r0.x, c[VSC_UTIL].z       // clamp fog to > 0.0
min oFog, r0.x, c[VSC_UTIL].x     // clamp fog to < 1.0
#endif


#ifdef VS_FOG_SKIN2_NO
// Fog
mov oFog, c[VSC_UTIL].x     // clamp fog to < 1.0
#endif
