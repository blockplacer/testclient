#version 330 core

layout(location = 0) in vec3 pos;
layout(location = 1) in vec3 col;
layout(location = 2) in vec2 tex;
layout(location = 3) in vec3 nx;
//we have to pass the vertex color to the fragment so
out vec3 vertexColor;
out vec2 texCoord;//2
out vec3 frag_position;
uniform vec3 viewPos;
uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;
out vec3 Lighting;
void main(void)
{
    vertexColor = col;
    texCoord = tex;
    frag_position = vec3(model * vec4(pos, 1.0f));
    gl_Position = vec4(pos, 1.0) * model * view * projection   ;
vec3 lightColor = vec3(1,1,1); 
vec3 lightPos = vec3(1,1,1);
 vec3 Position = vec3(model * vec4(pos, 1.0));
    vec3 Normal = mat3(transpose(inverse(model))) * nx;
    
    // ambient
    float ambientStrength = 0.1;
    vec3 ambient = ambientStrength * lightColor;
  	
    // diffuse 
    vec3 norm = normalize(Normal);
    vec3 lightDir = normalize(lightPos - Position);
    float diff = max(dot(norm, lightDir), 0.0);
    vec3 diffuse = diff * lightColor;
    
    // specular
    float specularStrength = 1.0; // this is set higher to better show the effect of Gouraud shading 
    vec3 viewDir = normalize(viewPos - Position);
    vec3 reflectDir = reflect(-lightDir, norm);  
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), 32);
    vec3 specular = specularStrength * spec * lightColor;      

Lighting =    ambient + diffuse + specular;// -ambient  //specular
}