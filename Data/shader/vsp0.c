/////////////////////////////////////////////////////// Version
#ifdef VS_VER_1_1
vs.1.1
#endif


/////////////////////////////////////////////////////// Declare
#ifdef VS_DCL_PARTICLE
dcl_position v0
dcl_color0 v3
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
#define VSC_ENV_TEXTURE_MATRIX			17          // 4°³
#define VSC_WORLD_POS					21			// 1°³
#define VSC_HEIGHTFOG					22
#define VSC_TEXTUREANI_MATRIX			23
#define VSC_COMMON						26

def c[VSC_UTIL],  1.0f,  0.5f, 0.0f, 765.01f
#endif

/////////////////////////////////////////////////////// Position

#ifdef VS_POS_NORMAL_SKIN0
m4x4 oPos,v0,c[VSC_WORLD_VIEW_PROJECTION]
#endif



/////////////////////////////////////////////////////// Light

#ifdef VS_LIGHT_PARTICLE
// Do the lighting calculation(Directional)
mov oD0, v3
#endif


/////////////////////////////////////////////////////// Texture UV

#ifdef VS_UV_SKIN0_TEXTURE0
// Copy texture coordinate
mov oT0.xy, v4.xy
#endif
