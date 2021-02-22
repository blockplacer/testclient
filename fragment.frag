#version 330 core

in vec3 vertexColor;
in vec3 frag_position;
out vec4 outputColor;

in vec2 texCoord;
uniform sampler2D texture0;
uniform float time;
uniform float Zcoord;
float vv;
void main()
{
vv+time;
  vec4 texel_color = texture(texture0,texCoord);//*
vec3 light_color = vec3(0,1,1);//+gl_FragCoord.w
  float dist = distance(vec3(0,1,1), frag_position);
 // float attenuation =  max((1.0f / (dist * dist)), 0.25f) ;//-frag_position.z
//float attenuation_ =  max((9.0f / (dist-frag_position.x/3 * dist)), 0.25f) ;
vec4 texel = vec4(floor(texel_color.r / 0.1f)*1,floor(texel_color.g / 0.1f)*0.1f,floor(texel_color.b / 0.1)*0.1f,1.0f);
  outputColor =   texel*vertexColor *  distance(frag_position.z/30,gl_FragCoord.z/3) ;//_color
}