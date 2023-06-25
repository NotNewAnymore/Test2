using Godot;
using System;

namespace Test2
{
	public partial class Labels : Label
	{
		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)		//Score and lives panel.
		{
			this.Text = $"Score = {Data.score}\n" +
				$"Lives = {Data.lives}\n" +
				$"Highscore = {Data.highscore}\n" +
				$"Scoreboard:";
			for (int i = 0; i < Data.playerdata.Count && i<10; i++)		//Display the list of scores.
			{
				this.Text += $"\n{Data.playerdata[i]}";
			}
		}
	}
}
