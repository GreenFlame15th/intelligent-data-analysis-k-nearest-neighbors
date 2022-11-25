using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace lab
{
    public class FileReader
    {
        private String path;

        public List<String> GetFiles()
        {
            return Directory.GetFiles(path).ToList<String>();
        }

        public List<Text> FilesToTextWithStrings(List<String> list, int minChar)
        {
            List<Text> toReturn = new List<Text>();

            foreach (String file in list)
            {
                List<String> parts = Regex.Split(File.ReadAllText(file), @"(<REUTERS[\s\S]+?<\/REUTERS>)").Where(l => l != string.Empty).ToList();
                parts.RemoveAt(0); //remove title

                foreach (String part in parts)
                {
                    //find body
                    List<String> bodyStrs = Regex.Split(part, @"(<BODY>[\s\S]+?<\/BODY>)").Where(l => l != string.Empty).ToList();
                    //in case there wasno body, skipping in bettwen parts
                    if (bodyStrs.Count > 1)
                    {
                        try
                        {
                            Text text = new Text();
                            List<String> placesParts = Regex.Split(part, @"(<PLACES>[\s\S]+?<\/PLACES>)").Where(l => l != string.Empty).ToList();
                            if (placesParts.Count > 1)
                            {
                                //find places
                                text.places = Regex.Split(
                                    placesParts[1]
                                , @"(<D>[\s\S]+?<\/D>)").Where(l => l != string.Empty).ToList();
                                //remove <Places> tag
                                text.places.RemoveAt(0);
                                text.places.RemoveAt(text.places.Count - 1);
                                //trim <D> tags
                                for (int i = 0; i < text.places.Count; i++)
                                {
                                    text.places[i] = text.places[i].Remove(text.places[i].Length - 4, 4).Remove(0, 3);
                                }
                            }
                            //trim tags & common parts from body
                            text.body = bodyStrs[1].Remove(bodyStrs[1].Length - 7, 7).Remove(0, 6);
                            if(text.body.Length >= minChar)
                            toReturn.Add(text);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("erroe:");
                            Console.WriteLine("in " + file);
                            Console.WriteLine(e);
                            Console.WriteLine("####");
                            Console.WriteLine("in:");
                            Console.Write(part);
                        }
                    }
                }
            }
            return toReturn;
        }

        public FileReader(String path)
        {
            this.path = path;
        }
    }
}
