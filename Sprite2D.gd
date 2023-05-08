extends Sprite2D

var speed = 10
var velocity
var animplayer

func _process(_delta):
	##animplayer = get_node("AnimationPlayer")
	if Input.is_action_pressed("Focus"):
		speed = 3
	else:
		speed=8
		
	##if Input.is_action_pressed("Fire_1"):
		
	var input_direction = Input.get_vector("left", "right", "up", "down")
	velocity = input_direction * speed
	position += velocity
	
	if position.x > 600:
		position = Vector2(600,position.y)
	if position.x < 0:
		position = Vector2(0,position.y)

	if position.y > 700:
		position = Vector2(position.x,700)
	if position.y < 0:
		position = Vector2(position.x,0)
