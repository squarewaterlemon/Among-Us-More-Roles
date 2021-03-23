using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using UnhollowerBaseLib;
using UnityEngine;

namespace AmongUsMoreRolesMod.Util
{
    public static class Utilities
    {
        public static readonly Version Version = new Version(1, 0, 0);

        public static bool IsRegularTask(TaskTypes task)
        {
            switch (task)
            {
                case TaskTypes.FixComms:
                case TaskTypes.FixLights:
                case TaskTypes.ResetReactor:
                case TaskTypes.ResetSeismic:
                case TaskTypes.RestoreOxy:
                    return false;
                default:
                    return true;
            }
        }
        public static Sprite LoadSpriteFromAssemblyResource(Assembly assembly, string resource)
        {
            var tex = LoadTextureFromAssemblyResource(assembly, resource);

            return Sprite.Create(
                tex,
                new Rect(0, 0, tex.width, tex.height),
                new Vector2(0.5f, 0.5f)
            );
        }
        public static Texture2D LoadTextureFromAssemblyResource(Assembly assembly, string resource)
        {
            try
            {
                var texture = assembly.GetManifestResourceStream(resource);
                Texture2D tex = new Texture2D(2, 2, TextureFormat.ARGB32, false);
                byte[] hatTexture = new byte[texture.Length];
                texture.Read(hatTexture, 0, (int)texture.Length);
                LoadImage(tex, hatTexture, false);

                return tex;
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message + "\n" + e.StackTrace);
                throw e;
            }
        }

        public static Texture2D LoadTextureFromFile(string filepath)
        {
            try
            {
                using (var stream = new StreamReader(filepath))
                {
                    var texture = stream.BaseStream;

                    Texture2D tex = new Texture2D(2, 2, TextureFormat.ARGB32, false);

                    byte[] textureArr = new byte[texture.Length];
                    texture.Read(textureArr, 0, (int)texture.Length);
                    LoadImage(tex, textureArr, false);

                    return tex;
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Error loading texture from file: " + e.Message);
                throw e;
            }
        }
        public static Sprite LoadSpriteFromFile(string filepath)
        {
            var tex = LoadTextureFromFile(filepath);

            return Sprite.Create(
                tex,
                new Rect(0, 0, tex.width, tex.height),
                new Vector2(0.5f, 0.5f)
            );
        }

        internal delegate bool d_LoadImage(IntPtr tex, IntPtr data, bool markNonReadable);
        internal static d_LoadImage iCall_LoadImage;
        public static bool LoadImage(Texture2D tex, byte[] data, bool markNonReadable)
        {
            if (iCall_LoadImage == null)
                iCall_LoadImage = IL2CPP.ResolveICall<d_LoadImage>("UnityEngine.ImageConversion::LoadImage");

            var il2cppArray = (Il2CppStructArray<byte>)data;

            return iCall_LoadImage.Invoke(tex.Pointer, il2cppArray.Pointer, markNonReadable);
        }

        public static PlayerControl GetClosestPlayer(PlayerControl from)
        {
            double min = double.MaxValue;
            PlayerControl closest = null;
            foreach (PlayerControl player in PlayerControl.AllPlayerControls) 
            {
                if (player.PlayerId == from.PlayerId) continue;
                double d = DistanceBetweenPlayers(from, player);
                if (d < min)
                {
                    min = d;
                    closest = player;
                }
            }
            return closest;
        }
        public static double DistanceBetweenPlayers(PlayerControl p1, PlayerControl p2)
        {
            return Dist(p1.GetTruePosition(), p2.GetTruePosition());
        }
        public static double Dist(Vector2 v1, Vector2 v2)
        {
            return Math.Sqrt(Math.Pow(v1.x - v2.x, 2) + Math.Pow(v1.y - v2.y, 2));
        }
        public static double DistanceToClosestPlayer(PlayerControl player)
        {
            return DistanceBetweenPlayers(GetClosestPlayer(player), player);
        }

        public static PlayerControl GetPlayerById(byte id)
        {
            foreach (PlayerControl player in PlayerControl.AllPlayerControls)
            {
                if (player.PlayerId == id)
                {
                    return player;
                }
            }
            return null;
        }

        public static List<PlayerControl> PlayerControls;
    }
}
