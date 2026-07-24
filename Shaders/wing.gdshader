shader_type canvas_item;
render_mode world_vertex_coords;

varying vec2 world_position;
varying mat4 model;

void vertex() {
	world_position = VERTEX.xy;
	model = MODEL_MATRIX;
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
	wing_bg(COLOR);
	COLOR = floor(COLOR * 32.0) / 35.0;
}
