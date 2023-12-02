using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using GameItem;
using UnityEngine;

namespace Logic
{
    /// <summary>
    /// Object to retrieve ingredient value data from an XML file.
    /// If the file is not found, it will be generated from default Scriptable Object data
    /// </summary>
    public class IngredientItemXMLParser
    {
        private string DestinationFile => Application.persistentDataPath + "/" + _fileName;
        
        private const string _fileName = "IngredientWorth.xml";
        
        //object that retrieves information about objects from an XML file
        public Dictionary<string, int> Pars( IngredientItemsData defaultValue)
        {
            Dictionary<string, int> ingredientsNameWorth = new Dictionary<string, int>();
            if (File.Exists(DestinationFile) == false)
            {
                CreateNewXMLFile(GetDefaultIngredientCollection(defaultValue));
            }
            
            XmlSerializer serializer = new XmlSerializer(typeof(IngredientCollection));
            StringReader reader = new StringReader(File.ReadAllText(DestinationFile));
            IngredientCollection ingredientCollection = serializer.Deserialize(reader) as IngredientCollection;

            foreach (var ingredient in ingredientCollection.Ingredients)
            {
                ingredientsNameWorth.Add(ingredient.Name, ingredient.Worth);
            }
            
            return ingredientsNameWorth;
        }

        private void CreateNewXMLFile(IngredientCollection defaultIngredientCollection)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(IngredientCollection));
            StreamWriter stream = new StreamWriter(Application.persistentDataPath + "/" + _fileName);
            
            serializer.Serialize(stream, defaultIngredientCollection);
            stream.Close();
        }

        private IngredientCollection GetDefaultIngredientCollection(IngredientItemsData ingredientItemsData)
        {
            List<Ingredient> defaultIngredientsValue = new();
            foreach (var ingredientData in ingredientItemsData.IngredientItemData)
            {
                Ingredient newIngredient = new Ingredient();
                newIngredient.Name = ingredientData.Name;
                newIngredient.Worth = ingredientData.DefaultWorth;
                defaultIngredientsValue.Add(newIngredient);
            }

            IngredientCollection ingredientCollection = new IngredientCollection();
            ingredientCollection.Ingredients = defaultIngredientsValue;
            
            return ingredientCollection;
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
        [XmlElement("Name")]
        public string Name;
        
        [XmlElement("Worth")]
        public int Worth;
    }
}