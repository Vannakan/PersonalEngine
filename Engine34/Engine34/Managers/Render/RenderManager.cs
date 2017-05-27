using Engine.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Engine.Managers.Cam;
using Engine.Interfaces.Entities;
using Engine.Interfaces.ServiceLocator;
using Engine.Managers.ServiceLocator;
using Engine.Managers.Entities;
using Engine.Interfaces.Screen;
using Engine.Interfaces.Render;
using Engine.Interfaces.Cam;
using Engine34.Interfaces.Render;
using Engine34.Managers.Render;

namespace Engine.Managers.Render
{
    /// <summary>
    /// CLASS: The Render manager is responsible for the drawing of content loaded by the various other managers
    /// </summary>
    public class RenderManager : IRenderManager, ILightMaskManager
    {

        RenderTarget2D lightsTarget;
        RenderTarget2D mainTarget;
        Effect lightingShader;
        private bool light = false;


        ///TOO BE
        /// <summary>
        /// DECLARE: A queue of things to be drawn
        /// </summary>
        private Queue<string> LinesToBeDrawn = new Queue<string>();
        private Queue<GameText> TextToBeDrawn = new Queue<GameText>();
        private Queue<IDrawableComponent> Shapes = new Queue<IDrawableComponent>();
        private Queue<IDrawableComponent> ShapesOutOfCam = new Queue<IDrawableComponent>();
        public List<IDrawableComponent> shaders = new List<IDrawableComponent>();
        public List<ILightMask> LightMasks = new List<ILightMask>();

        /// <summary>
        /// DECLARE: A list of entities to be drawn
        /// </summary>
        private List<IEntity> entities = new List<IEntity>();
        ///DECLARE :Reference to the kernels spritebatch in which all entities will be drawn

        private SpriteBatch SpriteB;
        public SpriteBatch spriteBatch { get { return SpriteB; } set { SpriteB = value; } }
        ///DECCLARE: List containing items/entities that arent drawn within the cameras perams
        private List<IDrawableComponent> Drawables = new List<IDrawableComponent>();

        public List<IDrawableComponent> getD
        {
            get
            {
                return Drawables;
            }
        }
        public List<IDrawableComponent> getD1
        {
            get
            {
                return CamDrawables;
            }
        }

        public bool DrawLight { get { return light; } set { light = value; } }


        /// <summary>
        /// At the moment objects that wish to have a shader applied to them have to have the logic within the object
        /// eventually I aim to have one draw that iterates through all available shaders and draws the objects that wish to be drawn within that shader
        /// unless i come up with something better
        /// </summary>
        public void DrawShaders()
        {
            //Constants.g.SetRenderTarget(lightsTarget);
            for (int i = 0; i < shaders.Count; i++)
            {
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

                IShader test = shaders[i] as IShader;
                test.effect.CurrentTechnique.Passes[0].Apply();
                shaders[i].Draw(spriteBatch);           
                spriteBatch.End();

            }

        }

        /// <summary>
        /// Add a lightmask to the list
        /// </summary>
        /// <param name="mask"></param>
        public void addMask(ILightMask mask)
        {
            LightMasks.Add(mask);
        }

        /// <summary>
        /// Remove a lightmask from the list
        /// </summary>
        /// <param name="mask"></param>
        public void RemoveLightSource(ILightMask mask)
        {
           for(int i = 0; i < LightMasks.Count; i ++)
            {
                if (LightMasks[i] == mask)
                    LightMasks.RemoveAt(i);
            }
        }

        /// <summary>
        /// Draw all lightmasks with in the list
        /// </summary>
        public void DrawLightMasks()
        {
           //Set the render target to our light map
                Constants.g.SetRenderTarget(lightsTarget);
            Constants.g.Clear(Color.Black );

            //Begin drawing
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive,null,null,null,null, Locator.Instance.getService<ICameraManager>().getCam().get_transformation(Constants.g));
                for (int i = 0; i < LightMasks.Count; i++)
                {
                    LightMasks[i].Draw(spriteBatch);
                }
                //End Drawing
                spriteBatch.End();
        }

        /// <summary>
        /// Clear the lightmask list
        /// </summary>
        public void ClearLightMasks()
        {
            LightMasks.Clear();
        }

        /// <summary>
        /// Add a shader object to the shader list
        /// </summary>
        /// <param name="add"></param>
        public void AddShader(IDrawableComponent add)
        {
            shaders.Add(add);
            Console.WriteLine("ADDED");
        }

        ///DECLARE: List containing items/entities which will be drawn with cam perams
        private List<IDrawableComponent> CamDrawables = new List<IDrawableComponent>();

        private List<IEntity> CamDrawEntities = new List<IEntity>();
        private bool beginLight;



        /// <summary>
        /// CONSTRUCTOR
        /// </summary>
        public RenderManager()
        {
            var pp = Constants.g.PresentationParameters;
            lightsTarget = new RenderTarget2D(
                Constants.g, pp.BackBufferWidth, pp.BackBufferHeight);
            mainTarget = new RenderTarget2D(
                Constants.g, pp.BackBufferWidth, pp.BackBufferHeight);

           // DrawLight = true;
        }

        /// <summary>
        /// METHOD: An initialize method that is called every time a new
        ///screen is ready to be drawn
        /// </summary>
        public void Initialize()
        {
            getEntityList();

  

        }

        /// <summary>
        /// METHOD: gets the entity list from the Entity Manager
        /// </summary>
        public void getEntityList()
        {
            entities = Locator.Instance.getService<IEntityManager>().getList();
        }

        /// <summary>
        /// METHOD: gets the entity list from the Entity Manager
        /// </summary>
        public void getCamEntityList()
        {
            CamDrawEntities = Locator.Instance.getService<IEntityManager>().getCamList();
        }

        /// <summary>
        /// METHOD: For items which dont wish to be drawn in regards to the cameras matrix translations (such as GUI)
        /// </summary>
        /// <param name="d">The object to be drawn</param>
        public void addDrawable(IDrawableComponent d)
        {
            Drawables.Add(d);
        }

        /// <summary>
        /// METHOD: For Scenery/Entities which wish to be drawn in regards to the cameras matrix translations
        /// </summary>
        /// <param name="d">the object to be drawn</param>
        public void addCamDrawable(IDrawableComponent d)
        {
            CamDrawables.Add(d);
        }

        /// <summary>
        /// METHOD: For Entities which wish to be drawn in regards to the cameras matrix translations
        /// </summary>
        /// <param name="d">The object to be drawn</param>
        public void addCamDrawEntity(IDrawableComponent d)
        {
            CamDrawEntities.Add(d as IEntity);
        }



        /// <summary>
        /// METHOD: Draws everything in the camera view
        /// </summary>
        public void DrawCameraRelatedArtefacts()
        {

            Constants.g.SetRenderTarget(mainTarget);
            Constants.g.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.Deferred,
             BlendState.AlphaBlend,
             null,
             null,
             null,
             null,
             Locator.Instance.getService<ICameraManager>().getCam().get_transformation(Constants.g));
            DrawComponents();
            DrawCamDrawables();
            DrawCamDrawEntities();
           
            ///Below method should be in noncamera
            DrawShapes();


            spriteBatch.End();
        }

        /// <summary>
        /// METHOD: Clears the entities to be drawn in relation to the camear
        /// </summary>
        public void clearTempEntity()
        {
            CamDrawEntities.Clear();
        }

        /// <summary>
        /// METHOD: Draws everything not related to the camera
        /// </summary>
        public void DrawNonCameraRelatedArtefacts()
        {
            Constants.g.SetRenderTarget(mainTarget);  
            spriteBatch.Begin();
            DrawDrawables();
            DrawEntities();
            spriteBatch.End();

        }

        //// <summary>
        //// METHOD: Queue text to be drawn
        //// </summary>
        //// <param name="gameText">The text to be drawn</param>
        public void addString(GameText gameText)
        {
            TextToBeDrawn.Enqueue(gameText);
        }

        //// <summary>
        //// METHOD: Add a shape to the render managers shape draw list
        //// </summary>
        //// <param name="shape">The shape to be drawn</param>
        public void addShape(IDrawableComponent shape)
        {
            if (shape != null)
                Shapes.Enqueue(shape);
        }

        public void Remove(IDrawableComponent draw)
        {
         if(CamDrawables.Contains(draw))
            {
                CamDrawables.Remove(draw);
            }
        }

        /// <summary>
        /// METHOD: The update loop cycled through on each frame
        /// </summary>
        /// <param name="gameTime">Monogame GameTime property</param>
        public void Update(GameTime gameTime)
        {
            ///Update lists
            getEntityList();
            getCamEntityList();
        }

        /// <summary>
        /// Draw any lightmasks in the list
        /// </summary>
        public void DrawLighting()
        {
            if (LightMasks.Count > 0)
            {
                DrawLightMasks();
            }
        }

        /// <summary>
        /// Apply the lighting to the scene by passing in the 2D lighting shader 
        /// </summary>
        public void ApplyLightingToScene()
        {
            Constants.g.SetRenderTarget(null);
            Constants.g.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            if (DrawLight && LightMasks.Count > 0)
            {
                lightingShader.Parameters["myTex2D"].SetValue(lightsTarget);
                lightingShader.CurrentTechnique.Passes[0].Apply();
            }

            spriteBatch.Draw(mainTarget, Vector2.Zero, Color.White);

            spriteBatch.End();



        }

        /// <summary>
        /// METHOD: Draws items
        /// </summary>
        public void Draw()
        {

           if(lightingShader == null)
                lightingShader = Constants.cm.Load<Effect>("LightingShader");


            DrawLighting();
            DrawCameraRelatedArtefacts();
            DrawNonCameraRelatedArtefacts();
            DrawShaders();


            ApplyLightingToScene();



        }

        /// <summary>
        /// METHOD: Draws items
        /// </summary>
        public void Draw(Texture2D texture, Rectangle rect, Color col)
        {
            spriteBatch.Begin(SpriteSortMode.Immediate, null);
            spriteBatch.Draw(texture, rect, col);
            spriteBatch.End();
        }

        //// <summary>
        //// METHOD: Draws all entities that are currently waiting to be drawn
        //// </summary>
        public void DrawEntities()
        {
            foreach (IEntity i in entities)
            {
                if (i.isVisible)
                {
                    i.Draw(spriteBatch);
                }
            }
        }

        //// <summary>
        //// METHOD: Draws any components that require a draw
        //// </summary>
        public void DrawComponents()
        {
            Locator.Instance.getService<IScreenManager>().Draw(spriteBatch);

        }


        //// <summary>
        //// METHOD: Iterate through the shapes list and draw any shapes
        //// </summary>
        public void DrawShapes()
        {
            for (int i = 0; i < Shapes.Count; i++)
            {
                Shapes.Dequeue().Draw(spriteBatch);

            }
        }

        /// <summary>
        /// METHOD: Draws every Drawable object
        /// </summary>
        public void DrawDrawables()
        {
            for (int i = 0; i < Drawables.Count; i++)
            {
                Drawables[i].Draw(spriteBatch);
            }

            for (int i = 0; i < TextToBeDrawn.Count; i++)
            {
                GameText a = TextToBeDrawn.Dequeue();
                a.Draw(spriteBatch);
            }
        }

        /// <summary>
        /// METHOD: DRaws everything in relation to the camera
        /// </summary>
        public void DrawCamDrawables()
        {
            for (int i = 0; i < CamDrawables.Count; i++)
            {
                CamDrawables[i].Draw(spriteBatch);
            }
        }

        /// <summary>
        /// METHOD: draws entities in relation to the camera
        /// </summary>
        public void DrawCamDrawEntities()
        {
            for (int i = 0; i < CamDrawEntities.Count; i++)
            {
                CamDrawEntities[i].Draw(spriteBatch);
            }
        }

        public void Destroy(IEntity ent)
        {
            if (entities.IndexOf(ent) != -1)
            {
                entities.Remove(ent);
            }

            if (Drawables.IndexOf((IDrawableComponent) ent) != -1)
            {
                Drawables.RemoveAt(Drawables.IndexOf((IDrawableComponent)ent));
            }

            if (CamDrawables.IndexOf((IDrawableComponent)ent) != -1)
            {
                CamDrawables.RemoveAt(Drawables.IndexOf((IDrawableComponent)ent));
            }

            if (CamDrawEntities.IndexOf(ent) != -1)
            {
                CamDrawEntities.Remove(ent);
            }
        }
    }
}