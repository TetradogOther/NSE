using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Plugins;
using System.Windows.Forms;
using OWEditor;

namespace OWEditor
{
    public class Plugin : Plugins.PluginBase, IPlugin
    {
        private MainForm m_form;
        private string m_strName;
        private string m_strAuthor;
        private string m_strVersion;
        private PluginType type;
        private PluginClassification clas;

        

        public Plugin()
        {
            m_strName = "OW Editor";
            m_strAuthor = "Link12552";
            m_strVersion = "0.1";
            type = PluginType.Form;
            clas = PluginClassification.ModifyData;

        }
        public override PluginType Type
        {
            get { return type; }
            set { type = value; }
        }

        public override PluginClassification Classification
        {
            get
            {
                return clas;
            }
            set
            {
                clas = value;
            }
        }

        public override string Name 
        {
            get { return m_strName; }
            set { m_strName = value; }
        }
        public override string Author
        {
            get { return m_strAuthor; }
            set { m_strAuthor = value; }
        }
        public override string Version
        {
            get { return m_strVersion; }
            set { m_strVersion = value; }
        }
        public override Form Mainform
        {
            get { return m_form; }
            set { m_form = (MainForm)value; }
        }

        public override void Load()
        {
            if (!OWEditor.Program.opened)
            {
                m_form = new MainForm(this.Host);
                OWEditor.Program.opened = true;
            }

            
        }

        public override void Initialize()
        {
            
        }



    }
}
