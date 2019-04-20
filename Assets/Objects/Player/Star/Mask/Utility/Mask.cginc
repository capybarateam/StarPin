// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

struct appdata
{
	float4 vertex : POSITION;
	float3 normal : NORMAL;
};

struct v2f
{
	float4 pos : SV_POSITION;
	float3 normal : TEXDCOORD1;
};

v2f vert(appdata i)
{
	v2f o = (v2f)0;
	o.pos = UnityObjectToClipPos(i.vertex);
	o.normal = mul(unity_ObjectToWorld, float4(i.normal.xyz, 0.0)).xyz;
	return o;
}