using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Core;

namespace Logic
{
    public class IngredientXMLParser
    {
        private const string _fileName = "IngredientValue.xml";
        
        //object that retrieves information about objects from an XML file
        public bool TryPars( FileService fileService, out List<Ingredient> ingredients)
        {
            if (fileService.IfFileExists(_fileName))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(IngredientCollection));
                StringReader reader = new StringReader(fileService.GetFileData(_fileName));
                IngredientCollection ingredientCollection =
                    serializer.Deserialize(reader) as IngredientCollection;

                ingredients = ingredientCollection.Ingredients;
                reader.Close();
                return true;
            }

            ingredients = default;
            return false;
        }
    }

    [XmlRoot("IngedientCollection")]
    public class IngredientCollection
    {
        [XmlArray("Ingredients")]
        [XmlArrayItem("Ingredient")]
        public List<Ingredient> Ingredients = new();
    }
    
    public class Ingredient
    {
        [XmlAttribute("name")]
        public string Name;
        
        [XmlElement("Value")]
        public int Value;
    }
}