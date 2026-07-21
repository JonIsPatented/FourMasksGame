shader_type canvas_item;
render_mode world_vertex_coords;

varying vec2 world_position;
varying mat4 model;

global uniform bool demon;

void vertex() {
	world_position = VERTEX.xy;
	model = MODEL_MATRIX;
}

void demon_bg(inout vec4 _COLOR) {
	vec2 modv = (model * vec4(1)).xy;
	float lavaheight = modv.y * 0.9 + 10.0;
	vec2 l3pos = world_position - modv * 0.9;
	vec3 lattice_col = vec3(0.5, 0.1, 0.1);
	float lattice_val = sin(l3pos.x / 30.0);
	vec3 lightning_col = sin(TIME) * vec3(1, 0, 0.7);
	vec3 lattice = lattice_val * lattice_col;
	float l_sc = max(lattice.r + lattice.g + lattice.b, 0);
	_COLOR.rgb = lattice + 0.2 * (1.0 - l_sc) * lightning_col;
	lavaheight = lavaheight + l_sc * 20.0 * (1.0 - modv.y * 0.002);
	_COLOR.r = _COLOR.r * min(max(lattice_val * 400.0, 1), 0) + 0.5 * max(min(world_position.y - lavaheight - 20.0, 20.0), 0) / 20.0;
}

void wing_bg(inout vec4 _COLOR) {
	vec2 modv = (model * vec4(1)).xy;
	float cloudheight = modv.y * 0.9 - 120.0;
	float cloudpos = max(0.0, min(1.0, (world_position.y - cloudheight) / 200.0));
	vec4 sky_col = vec4(0.4, 0.8, 1, 1.0);
	vec4 cloud_col = vec4(1);
	_COLOR = (1.0 - cloudpos) * cloud_col + cloudpos * sky_col;
}

void fragment() {
	if (demon) {
		demon_bg(COLOR);
	} else {
		wing_bg(COLOR);
	}
	COLOR = floor(COLOR * 32.0) / 35.0;
}
