using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pong;

public class Game1 : Game
{
    Texture2D paddleTexture;
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Rectangle paddleRect1;
    private Rectangle paddleRect2;
    private int windowWidth;
    private int windowHeight;
    private int paddleSpeed = 5;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        windowWidth = Window.ClientBounds.Width;
        windowHeight = Window.ClientBounds.Height;
        paddleRect1 = new Rectangle(10,120,30,90);
        paddleRect2 = new Rectangle(windowWidth - 50,120,30,90);
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        paddleTexture = new Texture2D(GraphicsDevice, 3, 3);
        paddleTexture.SetData<Color>(Enumerable.Repeat(Color.White, 3 * 3).ToArray());
        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        KeyboardState keyboard = Keyboard.GetState();
        if(keyboard.IsKeyDown(Keys.W)){
            paddleRect1.Y -= paddleSpeed;
        }
        else if(keyboard.IsKeyDown(Keys.S)){
            paddleRect1.Y += paddleSpeed; 
        }
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin(); 
        _spriteBatch.Draw(paddleTexture,paddleRect1, Color.White);
        _spriteBatch.Draw(paddleTexture,paddleRect2, Color.White);
        _spriteBatch.End(); 

        // TODO: Add your drawing code here

        base.Draw(gameTime);
    }
}
