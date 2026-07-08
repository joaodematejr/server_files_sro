/////////////////////////////////////////////////////// Version


/////////////////////////////////////////////////////// Declare

#ifdef VS_DCL_MAP_DUV1
dcl_position	v0;
dcl_color0		v7;						// v7 is diffuse color
dcl_texcoord0	v4;
#endif

#ifdef VS_DCL_MAP_UV1
dcl_position	v0;
dcl_texcoord0	v4;
#endif

#ifdef VS_DCL_MAP_UV3
dcl_position	v0;
dcl_texcoord0	v4;
dcl_texcoord1	v5;
#endif
/////////////////////////////////////////////////////// Define



/////////////////////////////////////////////////////// Position




/////////////////////////////////////////////////////// Light

#ifdef VS_LIGHT_MAP_DUV
mov oD0, v7
#endif

#ifdef VS_LIGHT_MAP_UV2
mov oD0, c[VSC_DLIGHT_AMBIENT]
#endif

/////////////////////////////////////////////////////// Texture UV


#ifdef VS_UV_MAP_SPLATTING
mov oT0.xy, v4.xy
;mov oT1.xy, v4.zw
mul oT1.xy, v4.zw, c[VSC_TEXTUREANI_MATRIX].x	; texture scale factor
#endif

#ifdef VS_UV_MAP_UV3
mov oT0.xy, v4.xy
mov oT1.xy, v4.zw
mov oT2.xy, v4.zw   //v5.xy
#endif
/////////////////////////////////////////////////////// Fog


