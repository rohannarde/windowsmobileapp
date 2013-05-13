/// <summary>
/// GamePage.xaml.cs
/// Author: Rohan Narde - rsn5700@rit.edu
/// Author: Amit Shroff - aas6521@rit.edu
/// </summary>
/// 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;


namespace CalorieCounterXNA
{
    /// <summary>
    /// This class is used to model the moving block based on the number of calories
    /// </summary>
    public partial class GamePage : PhoneApplicationPage
    {
        public const int numberOfTicks = 333333;
        // The current rectangle
        Texture2D texture;

        // A variety of rectangle colors
        Texture2D redTexture;
        Texture2D greenTexture;
        Texture2D blueTexture;
        
        // Used to move the rectangle around the screen
        Vector2 spritePosition;
        Vector2 spriteSpeed = new Vector2(100.0f, 100.0f);
        
        ContentManager contentManager;
        GameTimer timer;
        SpriteBatch spriteBatch;

        public GamePage()
        {
            InitializeComponent();

            // Get the content manager from the application
            contentManager = (Application.Current as App).Content;

            // Create a timer for this page
            timer = new GameTimer();
            timer.UpdateInterval = TimeSpan.FromTicks(numberOfTicks);
            timer.Update += OnUpdate;
            timer.Draw += OnDraw;
        }
       
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // Set the sharing mode of the graphics device to turn on XNA rendering
            SharedGraphicsDeviceManager.Current.GraphicsDevice.SetSharingMode(true);

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(SharedGraphicsDeviceManager.Current.GraphicsDevice);

            // TODO: use this.content to load your game content here
            // If texture is null, we've never loaded our content.
            if (null == texture)
            {
                redTexture = contentManager.Load<Texture2D>("redRect");
                greenTexture = contentManager.Load<Texture2D>("greenRect");
                blueTexture = contentManager.Load<Texture2D>("blueRect");

                // Start with the red rectangle.
                if (MainPage.totalCalories > 1000)
                {
                    texture = redTexture;
                }
                else if (MainPage.totalCalories < 1000 && MainPage.totalCalories >500)
                {
                    texture = blueTexture;
                }
                else 
                {
                    texture = greenTexture;
                }

            }

            // Start the timer
            timer.Start();

            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            // Stop the timer
            timer.Stop();

            // Set the sharing mode of the graphics device to turn off XNA rendering
            SharedGraphicsDeviceManager.Current.GraphicsDevice.SetSharingMode(false);

            base.OnNavigatedFrom(e);
        }

        /// <summary>
        /// Allows the page to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        private void OnUpdate(object sender, GameTimerEventArgs e)
        {
            // TODO: Add your update logic here
            // Move the sprite by speed, scaled by elapsed time.
            spritePosition += spriteSpeed * (float)e.ElapsedTime.TotalSeconds;

            int MinX = 0;
            int MinY = 0;
            int MaxX = SharedGraphicsDeviceManager.Current.GraphicsDevice.Viewport.Width - texture.Width;
            int MaxY = SharedGraphicsDeviceManager.Current.GraphicsDevice.Viewport.Height - texture.Height;

            // Check for bounce.
            if (spritePosition.X > MaxX)
            {
                spriteSpeed.X *= -1;
                spritePosition.X = MaxX;
            }

            else if (spritePosition.X < MinX)
            {
                spriteSpeed.X *= -1;
                spritePosition.X = MinX;
            }

            if (spritePosition.Y > MaxY)
            {
                spriteSpeed.Y *= -1;
                spritePosition.Y = MaxY;
            }
            else if (spritePosition.Y < MinY)
            {
                spriteSpeed.Y *= -1;
                spritePosition.Y = MinY;
            }


        }

        /// <summary>
        /// Allows the page to draw itself.
        /// </summary>
        private void OnDraw(object sender, GameTimerEventArgs e)
        {
            SharedGraphicsDeviceManager.Current.GraphicsDevice.Clear(Color.Black);

            // Draw the sprite
            spriteBatch.Begin();

            // Draw the rectangle in its new position
            spriteBatch.Draw(texture, spritePosition, Color.White);

            spriteBatch.End();
        }
    }
}