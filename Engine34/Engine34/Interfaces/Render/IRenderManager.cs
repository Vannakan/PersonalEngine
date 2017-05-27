using Engine.Interfaces.Entities;
using Engine.Interfaces.ServiceLocator;
using Engine.Utilities;
using Engine34.Managers.Render;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine.Interfaces.Render
{
    /// <summary>
    /// INTERFACE: This is the interface for the render manager which is responsible for all of the drawing of content in the
    /// program
    /// </summary>
    public interface IRenderManager : IUpdService
    {

        SpriteBatch spriteBatch { get; set; }
        /// <summary>
        /// METHOD: An initialize method that is called every time a new
        ///screen is ready to be drawn
        /// </summary>
        void Initialize();
        /// <summary>
        /// METHOD: gets the entity list from the Entity Manager
        /// </summary>
        void getEntityList();
        /// <summary>
        /// METHOD: gets the entity list from the Entity Manager
        /// </summary>
        void getCamEntityList();
        /// <summary>
        /// METHOD: For items which dont wish to be drawn in regards to the cameras matrix translations (such as GUI)
        /// </summary>
        /// <param name="d">The object to be drawn</param>
        void addDrawable(IDrawableComponent d);
        /// <summary>
        /// METHOD: For Scenery/Entities which wish to be drawn in regards to the cameras matrix translations
        /// </summary>
        /// <param name="d">the object to be drawn</param>
        void addCamDrawable(IDrawableComponent d);
        /// <summary>
        /// METHOD: For Entities which wish to be drawn in regards to the cameras matrix translations
        /// </summary>
        /// <param name="d">The object to be drawn</param>
        void addCamDrawEntity(IDrawableComponent d);
        /// <summary>
        /// METHOD: Draws everything in the camera view
        /// </summary>
        void DrawCameraRelatedArtefacts();
        /// <summary>
        /// METHOD: Clears the entities to be drawn in relation to the camera
        /// </summary>
        void clearTempEntity();
        /// <summary>
        /// METHOD: Draws everything not related to the camera
        /// </summary>
        void DrawNonCameraRelatedArtefacts();
        //// <summary>
        //// METHOD: Queue text to be drawn
        //// </summary>
        //// <param name="gameText">The text to be drawn</param>
        void addString(GameText gameText);
        //// <summary>
        //// METHOD: Add a shape to the render managers shape draw list
        //// </summary>
        //// <param name="shape">The shape to be drawn</param>
        void addShape(IDrawableComponent shape);
        /// <summary>
        /// METHOD: The update loop cycled through on each frame
        /// </summary>
        /// <param name="gameTime">Monogame GameTime property</param>
        void Update(GameTime gameTime);
        /// <summary>
        /// METHOD: Draws items
        /// </summary>
        void Draw();
        /// <summary>
        /// METHOD: Draws items
        /// </summary>
        void Draw(Texture2D texture, Rectangle rect, Color col);
        //// <summary>
        //// METHOD: Draws all entities that are currently waiting to be drawn
        //// </summary>
        void DrawEntities();
        //// <summary>
        //// METHOD: Draws any components that require a draw
        //// </summary>
        void DrawComponents();
        //// <summary>
        //// METHOD: Iterate through the shapes list and draw any shapes
        //// </summary>
        void DrawShapes();
        /// <summary>
        /// METHOD: Draws every Drawable object
        /// </summary>
        void DrawDrawables();
        /// <summary>
        /// METHOD: DRaws everything in relation to the camera
        /// </summary>
        void DrawCamDrawables();
        /// <summary>
        /// METHOD: draws entities in relation to the camera
        /// </summary>
        void DrawCamDrawEntities();

        void Destroy(IEntity ent);

        void Remove(IDrawableComponent draw);

        void AddShader(IDrawableComponent draw);

    }
}