using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Plugins
{
    public enum PluginType
    {
        MdiChildForm,
        Form,
        DialogForm,
        Internal
        
    }

    public enum PluginClassification
    {
        Create,
        EditSprite,
        ModifyData,
        GhostCreate,
        GhostEditSprite,
        GhostModifyData,

    }

    public class PluginBase : IPlugin
    {
        private string name = "Name";
        private string author = "Author";
        private string version = "1.0.0.0";
        private Form mainform;
        private PluginType type = PluginType.MdiChildForm;
        private PluginClassification classifation = PluginClassification.EditSprite;
        private IPluginHost host;

        public virtual PluginType Type
        {
            get { return type; }
            set { type = value; }
        }

        public virtual PluginClassification Classification
        {
            get { return classifation; }
            set {classifation = value;}
        }

        public virtual string Name
        {
            get { return name; }
            set { name = value; }
        }
        public virtual string Author
        {
            get { return author; }
            set { author = value; }
        }
        public virtual string Version
        {
            get { return version; }
            set { version = value; }
        }
        public virtual Form Mainform
        {
            get { return mainform; }
            set { mainform = value; }
        }

        public virtual void Load()
        {

        }
        public virtual void Initialize()
        {

        }
        public virtual void Dispose()
        {

        }

        public virtual IPluginHost Host
        {
            get { return host; }
            set
            {
                host = value;
                host.Register(this);
            }
        }
    }

    public interface IPlugin
    {
        IPluginHost Host { get; set; }

        string Name { get; set; }
        string Author { get; }
        string Version { get; }

        PluginType Type { get; }
        PluginClassification Classification { get; }
        Form Mainform { get; set;}

        void Load();
        void Initialize();
        void Dispose();
    }

    public interface IPluginHost
    {
        bool Register(IPlugin ipi);
        string Filename { get; }
        void SetEditor(ref NSE_Framework.Controls.Editor Editor);
        NSE_Framework.Controls.Editor currentEditor{ get; set;}
        void NewImport(string Name, EventHandler importEvent);
        void NewExport(string Name, EventHandler exportEvent);
        
        void NewEditorForm(NSE_Framework.IO.SpriteLibrary SpriteLibrary, string FileName);      
        void NewEditorForm(NSE_Framework.IO.SpriteLibrary SpriteLibrary);
        void NewEditorForm(NSE_Framework.Data.Sprite Sprite);
        void NewEditorForm(NSE_Framework.Data.Sprite Sprite, string FileName);

    }
}