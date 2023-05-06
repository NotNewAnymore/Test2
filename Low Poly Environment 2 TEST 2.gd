extends Node3D


var counter = 0
var countermax = 2000
# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(_delta):
	position += Vector3(0,0,0.1)
	counter += 1
	if(counter >= countermax):
		position -= Vector3(0 ,0 , countermax * 0.1)
		counter = 0
	pass
