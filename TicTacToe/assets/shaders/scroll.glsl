uniform sampler2D texture;
uniform vec2 resolution;
uniform float time;

void main()
{
    vec2 texCoord;
    texCoord.x = fract((gl_FragCoord.x / resolution.x) + time * 0.25);
    texCoord.y = fract((gl_FragCoord.y / resolution.y) + time * 0.1);
    gl_FragColor = texture2D(texture, texCoord);
}