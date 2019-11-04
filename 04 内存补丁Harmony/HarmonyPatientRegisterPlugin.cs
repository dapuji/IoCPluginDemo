using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using FxEcis.T0000.IoC;
using FxEcis.T0000.IoC.Config;
using FxEcis.T8000.Triage.Common;
using Harmony;

namespace IoCPluginDemo._04_内存补丁Harmony
{
    public class HarmonyPatientRegisterPlugin : BaseIoCPlugin
    {
        #region Overrides of BaseIoCPlugin

        /// <summary>初始化插件</summary>
        /// <param name="container">默认的<see cref="T:FxEcis.T0000.IoC.EcisContainer" />容器</param>
        public override void InitPlugin(EcisContainer container)
        {
            //这里用的是Harmony
            //使用的功能也会是相对来说比较简单的
            //项目说明是 https://github.com/pardeike/Harmony/wiki
            //有相关问题，可访问以上URL查看详细的文档
            var harmony = HarmonyInstance.Create("Ecis.Pacth");

            //这样是最方便注册补丁的方法。
            //但是这样也一样需要在补丁的类增加 HarmonyPatch 等属性
            //但是这样会存在一个问题，如果这个DLL有两个插件，都需要使用这个功能(两个插件都调用了该方法)，可能会导致补丁多次注册，从而执行了多次。
            //harmony.PatchAll(Assembly.GetExecutingAssembly());

            //以下写法比较麻烦，但是会更加稳定，不会影响到其它插件的运行
            var original = AccessTools.Method(typeof(PatientRegisterStrategy), "PatientRegister");
            var prefix = new HarmonyMethod(typeof(PatchPatientRegisterStaticMethod), "Prefix");
            var postfix = new HarmonyMethod(typeof(PatchPatientRegisterStaticMethod), "Postfix");
            var transpiler = new HarmonyMethod(typeof(PatchPatientRegisterStaticMethod), "Transpiler");
            harmony.Patch(original, prefix, postfix, transpiler);
        }

        /// <summary>该插件所属分类</summary>
        public override string Module { get; } = "Demo";

        /// <summary>插件名称</summary>
        public override string Name { get; } = "Triage_PrintAfterReregister";

        /// <summary>简单说明</summary>
        public override string Caption { get; } = "分诊，重新挂号后，重新打印分诊条";

        /// <summary>详细说明</summary>
        public override string Description { get; } = "分诊，重新挂号后，重新打印分诊条";

        /// <summary>详细说明图片</summary>
        public override Image DescriptionImage { get; } = null;

        /// <summary>插件配置项，用于插件数据的加载及保存</summary>
        public override List<PluginConfig> PluginConfigs { get; set; }

        #endregion
    }
}
