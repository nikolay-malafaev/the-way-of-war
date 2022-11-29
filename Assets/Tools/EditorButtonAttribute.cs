using System;

namespace Tools
{
    [AttributeUsage(AttributeTargets.Method)]
    public class EditorButtonAttribute : Attribute
    {
        public string name;
        
        
        public EditorButtonAttribute(string name)
        {
            
            this.name = name;
        }
    }
    
    [AttributeUsage(AttributeTargets.Method)]
    public class EditorButtonCreateAttribute : Attribute
    {
        public string name;
        
        
        public EditorButtonCreateAttribute(string name)
        {
            
            this.name = name;
        }
    }
    
    [AttributeUsage(AttributeTargets.Method)]
    public class EditorButtonRemoveAttribute : Attribute
    {
        public string name;
        
        
        public EditorButtonRemoveAttribute(string name)
        {
            
            this.name = name;
        }
    }
}