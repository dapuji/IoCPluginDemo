using System.Collections.Generic;
using FxEcis.T0000.IoC.Config;

namespace IoCPluginDemo._03_配置Config
{
    public partial class ConfigControl : BaseConfigControl
    {
        private List<PluginConfig> _configs;

        public ConfigControl()
        {
            InitializeComponent();
        }

        #region Overrides of BaseConfigControl

        /// <summary>获取已修改的配置</summary>
        /// <returns></returns>
        public override List<PluginConfig> GetConfig()
        {
            _configs[0].Value = textBox1.Text;
            _configs[1].Value = textBox2.Text;
            _configs[2].Value = textBox3.Text;
            return _configs;
        }

        /// <summary>载入配置</summary>
        /// <param name="configs"></param>
        public override void SetConfig(List<PluginConfig> configs)
        {
            _configs = configs;
            textBox1.Text = _configs[0].Value;
            textBox2.Text = _configs[1].Value;
            textBox3.Text = _configs[2].Value;
        }

        #endregion
    }
}
