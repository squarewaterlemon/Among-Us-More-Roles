using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace AmongUsMoreRolesMod.Util
{
    public class VisualArrow
    {
        GameObject gameObj;
        ArrowBehaviour arrow;

        public const string ARROW_RES_PATH = "AmongUsMoreRolesMod.Res.arrow.png";

        public static List<Tuple<int, VisualArrow>> arrows = new List<Tuple<int, VisualArrow>>();

        public VisualArrow(Vector3 target, int id)
        {
            gameObj = new GameObject { layer = 5 };
            arrow = gameObj.AddComponent<ArrowBehaviour>();
            arrow.target = target;
            SpriteRenderer renderer = gameObj.AddComponent<SpriteRenderer>();
            arrow.image = renderer;
            renderer.sprite = Utilities.LoadSpriteFromAssemblyResource(Assembly.GetExecutingAssembly(), ARROW_RES_PATH);
            arrows.Add(new Tuple<int, VisualArrow>(id, this));
        }

        public void ChangeTarget(Vector3 target)
        {
            arrow.target = target;
        }

        public static void Create(Vector3 target, int id)
        {
            new VisualArrow(target, id);
        }

        public static void ClearArrows()
        {
            arrows.Clear();
        }
        public static List<VisualArrow> GetArrows(int id)
        {
            return arrows.Where(x => x.Item1 == id).Select(x => x.Item2).ToList();
        }
    }
}
