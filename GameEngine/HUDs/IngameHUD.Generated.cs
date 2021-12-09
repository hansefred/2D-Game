/* Generated by MyraPad at 30.11.2021 20:26:54 */
using Myra;
using Myra.Graphics2D;
using Myra.Graphics2D.TextureAtlases;
using Myra.Graphics2D.UI;
using Myra.Graphics2D.Brushes;
using Myra.Graphics2D.UI.Properties;
using Microsoft.Xna.Framework;
using GameEngine.Model;

#if MONOGAME || FNA
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#elif STRIDE
using Stride.Core.Mathematics;
#else
using System.Drawing;
using System.Numerics;
#endif

namespace GameEngine.HUDs
{

	partial class IngameHUD: Grid
	{
		#region GameObject 
		private readonly Game1 _game;
		private readonly Player _player;

        #endregion

        #region UIComponent
		private Label _Score;
		private Label _Time;

		private HorizontalStackPanel _healthBar;
		private TextureRegion _healthTexture;

		#endregion

		public IngameHUD(Game1 game, Player player)
        {
			_game = game;
			_player = player;


		    _healthTexture = MyraEnvironment.DefaultAssetManager.Load<TextureRegion>(@"Content\Life.png");

			BuildUI();
        }

		public void Update (GameTime gametime)
        {
			
			_Score.Text = _game.GameData.Score.ToString();
			_Time.Text = _game.GameData.IngameTime.ToString(@"hh\:mm\:ss");


			_healthBar.Widgets.Clear();
			for (int i = 0; i <= _player.HitPoint; i++ )
            {
				_healthBar.Widgets.Add(new Image () { MinHeight = 10, MinWidth = 10, MaxHeight = 50, MaxWidth = 50 , Renderable = _healthTexture});

			}
        }

		private void BuildUI()
		{


			var VerticalStackPanel = new VerticalStackPanel();

			//Score
			_Score = new Label();
			_Score.Text = "0";
			var HorizontalStackPanel = new HorizontalStackPanel();
			HorizontalStackPanel.Widgets.Add(new Label() { Text = "Score: "});
			HorizontalStackPanel.Widgets.Add(_Score);

			//Time

			_Time = new Label();
			_Time.Text = "0";
			var HorizontalStackPanel1 = new HorizontalStackPanel();
			HorizontalStackPanel1.Widgets.Add(new Label() { Text = "Time: "});
			HorizontalStackPanel1.Widgets.Add(_Time);

			VerticalStackPanel.Widgets.Add(HorizontalStackPanel);
			VerticalStackPanel.Widgets.Add(HorizontalStackPanel1);




			_healthBar = new HorizontalStackPanel();
			_healthBar.GridRow = 2;



			
			GridLinesColor = ColorStorage.CreateColor(255, 204, 1, 255);
			RowsProportions.Add(new Proportion
			{
				Type = Myra.Graphics2D.UI.ProportionType.Part,
				Value = 5,
			});
			RowsProportions.Add(new Proportion
			{
				Type = Myra.Graphics2D.UI.ProportionType.Part,
				Value = 60,
			});
			RowsProportions.Add(new Proportion
			{
				Type = Myra.Graphics2D.UI.ProportionType.Part,
				Value = 5,
			});
			Widgets.Add(VerticalStackPanel);
			Widgets.Add(_healthBar);
		}

		
	}
}
