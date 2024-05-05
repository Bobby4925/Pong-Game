using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pong;

public class Game1 : Game
{
    Texture2D paddleTexture;
    Texture2D ballTexture;
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Rectangle paddleRect1;
    private Rectangle paddleRect2;
    private Rectangle ballRect;
    private int windowWidth;
    private int windowHeight;
    private int paddleSpeed = 5;
    private int ballSpeed = 5; 
    private int player;
    private int ballvx; 
    private int ballvy;
    private bool initial;
    private int dx; 
    private int dy;
    private int colAngle; 

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        var rand = new Random(); 
         player = rand.Next(1,3);
        initial = true; 
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        windowWidth = Window.ClientBounds.Width;
        windowHeight = Window.ClientBounds.Height;
        paddleRect1 = new Rectangle(10,120,30,90);
        paddleRect2 = new Rectangle(windowWidth - 50,120,30,90);
        ballRect = new Rectangle(windowWidth/2, windowHeight/2, 10, 10);
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        paddleTexture = new Texture2D(GraphicsDevice, 3, 3);
        paddleTexture.SetData<Color>(Enumerable.Repeat(Color.White, 3 * 3).ToArray());
        ballTexture = new Texture2D(GraphicsDevice, 3, 3);
        ballTexture.SetData<Color>(Enumerable.Repeat(Color.White, 3 * 3).ToArray());
        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        if(player == 1 && initial){
            ballvx -= ballSpeed;
            initial = false;
        }
        else if(player == 2 && initial){
            ballvx -= ballSpeed;
            initial = false; 
        }
        KeyboardState keyboard = Keyboard.GetState();
        if(keyboard.IsKeyDown(Keys.W)){
            paddleRect1.Y -= paddleSpeed;
        }
        else if(keyboard.IsKeyDown(Keys.S)){
            paddleRect1.Y += paddleSpeed; 
        }
        if(keyboard.IsKeyDown(Keys.Up)){
            paddleRect2.Y -= paddleSpeed;
        }
        else if(keyboard.IsKeyDown(Keys.Down)){
            paddleRect2.Y += paddleSpeed; 
        }
        ballRect.X += ballvx;
        ballRect.Y += ballvy;
        if(ballRect.Intersects(paddleRect1)){
            dx = paddleRect1.X - ballRect.X; 
            dy = paddleRect1.Y - ballRect.Y;
            colAngle = (int)Math.Atan2(dy,dx); 
            ballvx = (int) (ballSpeed * Math.Cos(colAngle));
            ballvy = (int) (ballSpeed * Math.Sin(colAngle));
            Console.WriteLine(Math.Sin(colAngle));
            Console.WriteLine("Collison!");
        }
        if(ballRect.Intersects(paddleRect2)){
            dx = paddleRect2.X - ballRect.X; 
            dy = paddleRect2.Y - ballRect.Y;
            colAngle = (int)Math.Atan2(dy,dx); 
            ballvx = (int) (-ballSpeed * Math.Cos(colAngle));
            ballvy = (int) (-ballSpeed * Math.Sin(colAngle));
            Console.WriteLine(Math.Sin(colAngle));
            Console.WriteLine("Collison!");
        }
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin(); 
        _spriteBatch.Draw(paddleTexture,paddleRect1, Color.White);
        _spriteBatch.Draw(paddleTexture,paddleRect2, Color.White);
        _spriteBatch.Draw(ballTexture,ballRect, Color.White);
        _spriteBatch.End(); 

        // TODO: Add your drawing code here

        base.Draw(gameTime);
    }
}
