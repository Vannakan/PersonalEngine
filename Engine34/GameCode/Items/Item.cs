using Engine.Interfaces.Collision;
using Engine.Interfaces.Resource;
using Engine.Managers.ServiceLocator;
using GameCode.Items.ENUMS;
using GameCode.Items.mStats;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using Engine.Interfaces.Entities;
using Engine34.Interfaces.Collision;
using Engine.Managers.Collision;
using Engine.Entities;
using GameCode.Entities.Platformer;
using Engine.Events.CollisionEvent;
using GameCode.Items.Management;
using Engine.Utilities;
using Microsoft.Xna.Framework.Graphics;
using Engine.Utility;
using Engine.Interfaces.Sound;

namespace GameCode.Items
{
   public class Item : Pickup, ICollidable
    {
        ItemManager parent;
      
    IMind mind;
        int index;
        public string name;
        ItemType type;
        protected Stats stats;
        protected string description;
        public Rarity rarity;
        // private List<IModifier> modifiers = new List<IModifier>();
        private List<IHitbox> hits;

        bool iscolliding = false;
        GameText text;

        string Texture;

        public Rectangle Bounds
        {
            get
            {
                return new Rectangle((int)Pos.X, (int)Pos.Y, Tex.Width, Tex.Height);
            }
        }

        public Vector2 Position
        {
            get
            {
                return Pos;
            }

            set
            {
                Pos = value;
            }
        }

        public bool isCollidable
        {
            get
            {
                return true;
            }

            set
            {
                
            }
        }

        public virtual void OnSATCollision(object sender, CollisionEventArgs e)
        {
            if(e.A == this && e.B.GetType() == typeof(PlayerMind) )
            {
                PlayerMind obj = (PlayerMind)e.B;
                PickUp(obj.stats);
                parent.RemoveItem(this);
                Locator.Instance.getService<IDetectionManager>().RemoveCollision(this);
            }
            else 
                  if (e.B == this && e.A.GetType() == typeof(PlayerMind))
                {
                    PlayerMind obj = (PlayerMind)e.A;
                    PickUp(obj.stats);
                    parent.RemoveItem(this);
                    Locator.Instance.getService<IDetectionManager>().RemoveCollision(this);
                }
        }

        public bool isColliding
        {
            get
            {
                return iscolliding;
            }

            set
            {
                iscolliding = value;
            }
        }

        public List<IHitbox> Hits
        {
            get
            {
                return hits;
            }

            set
            {
                hits = value;
            }
        }

        public Item(ItemManager e)
        {
            parent = e;
        }

        public override void Initialize(string name, Vector2 mpos, int radius)
        {
          
            mind = new TestMind();
            this.name = name;
            Pos = mpos;
            stats = new Stats();
            Load();
            Tex = Locator.Instance.getService<IResourceLoader>().GetTex(Texture);
            hits = new List<IHitbox>();
            hits.Add(new Hitbox(Pos, Tex.Width, Tex.Height, 0, mind));
            Locator.Instance.getService<IDetectionManager>().addCollidable(this);
            Locator.Instance.getService<IDetectionManager>().OnSATCollision += OnSATCollision;
            //RenderManager.Instance.addDrawable(this);

        }

      

        public override void PickUp(IStats mStats)
        {
            mStats.aSPD += stats.aSPD;
            mStats.mSPD += stats.mSPD;
            mStats.DMG += stats.DMG;
            mStats.HP += stats.HP;
            Locator.Instance.getService<ISoundManager>().PlayEffect("ItemEffect");

            Unload();
            base.PickUp(mStats);
        }

        public virtual void Generate()
        {

        }

        public void Unload()
        {
            Locator.Instance.getService<IDetectionManager>().RemoveCollision(this);
            Locator.Instance.getService<IDetectionManager>().OnSATCollision -= OnSATCollision;

        }

        public void Load()
        {
            string x = Directory.GetCurrentDirectory();
            string newstr = x.Replace("\\bin\\Windows\\Debug", "\\XmlFiles");
            XmlTextReader reader1 = new XmlTextReader(newstr+"\\ItemList.xml");
            while (reader1.Read())
            {
                reader1.ReadToFollowing("Item");
                reader1.MoveToFirstAttribute();
                string readname = reader1.Value;
                if (readname == name)
                {
                    reader1.ReadToFollowing("texture");
                    Texture = reader1.ReadElementContentAsString();
                    reader1.ReadToFollowing("aspd");
                    stats.aSPD = reader1.ReadElementContentAsInt();
                    reader1.ReadToFollowing("mspd");
                    stats.mSPD = reader1.ReadElementContentAsInt();
                    reader1.ReadToFollowing("hp");
                    stats.HP = reader1.ReadElementContentAsInt();
                    reader1.ReadToFollowing("dmg");
                    stats.DMG = reader1.ReadElementContentAsInt();
                    reader1.ReadToFollowing("description");
                    description = reader1.ReadElementContentAsString();

                    reader1.ReadToFollowing("rarity");
                    {
                        string scase = reader1.ReadElementContentAsString();
                        switch (scase)
                        {
                            case "Common":
                                rarity = Rarity.Common;
                                break;
                            case "Uncommon":
                                rarity = Rarity.Uncommon;
                                break;
                            case "Rare":
                                rarity = Rarity.Rare;
                                break;

                        }
                    }


                    Console.WriteLine("NAME: " + name);
                    Console.WriteLine("RARITY: " + rarity);
                    Console.WriteLine("DESC: " + description);


                }

            }
        }
        

        public void ApplyImpulse(Vector2 cVelocity)
        {
            throw new NotImplementedException();
        }

        public ICollidable getCollidable()
        {
            throw new NotImplementedException();
        }

        public IEntity getEntity()
        {
            throw new NotImplementedException();
        }

        public override void Draw(SpriteBatch spr)
        {
            float yOffset = 50;
            float xOffset = 50;

            DrawString.Draw(name, spr, new Vector2(Position.X ,Position.Y + yOffset), Color.White, 1f);
            base.Draw(spr);
        }
    }
}
