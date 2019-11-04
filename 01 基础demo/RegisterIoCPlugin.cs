using System;
using System.Collections.Generic;
using System.Drawing;
using FxEcis.T0000.IoC;
using FxEcis.T0000.IoC.Config;

namespace IoCPluginDemo._01_基础demo
{
    /// <summary>
    ///     演示如何IoC注册数据
    /// </summary>
    public class RegisterIoCPlugin : BaseIoCPlugin //这里必须继承BaseIoCPlugin或者IIoCPlugin才能发现该插件
    {
        #region Overrides of BaseIoCPlugin

        /// <summary>
        ///     初始化插件。
        ///     请勿在此方法调用<see cref="M:FxEcis.T0000.IoC.EcisContainer.Resolve(System.Type)" />、<see cref="M:FxEcis.T0000.IoC.EcisContainer.ResolveAll(System.Type)" />方法，这会导致容器锁定，后续的注册会失败。
        ///     如需要解析类型，应该使用<see cref="P:FxEcis.T0000.IoC.EcisContainer.EventAggregator" />获取<see cref="T:FxEcis.T0000.IoC.ContainerEndInitEvent" />事件，然后在事件触发后解析
        /// </summary>
        /// <param name="container">默认的<see cref="T:FxEcis.T0000.IoC.EcisContainer" />容器</param>
        public override void InitPlugin(EcisContainer container)
        {
            //这样是注册一个类型，每次解析时都会新建一个实例对象
            //container.Register<ISaveTriageInfo,SaveTriageInfo1>();

            //这样是注册到集合
            container.RegisterCollection<ISaveTriageInfo>(new []{typeof(SaveTriageInfo1),typeof(SaveTriageInfo2) });

            //SaveTriageInfo2构造参数内包含了ILogger
            //这里注册后，解析SaveTriageInfo2时，会自动传入Logger的实例
            //注册不分先后顺序
            container.Register<ILogger, Logger>();

            //注册到单个实现和注册到集合并不冲突。具体的解析，可看ResolveIoCPlugin
            //但是一般不会这样用，程序设计时，应该就确定一个接口能有多少个实现。
            //比如说，一个分诊保存的逻辑，只能有一个。但是，病历插入内容模块的接口，可以有插入医嘱、插入检查等等的实现。

            //这样注册，是单例模式，容器只会创建一个实例对象，然后将其缓存，每次解析都会返回这个实例。
            //container.RegisterSingleton<ISaveTriageInfo, SaveTriageInfo1>();

            //这样注册，也是单例模式，这样是自己创建一个实例，这种情况，适用于，构造函数中需要传入int,string等简单类型等情况
            //如果是构造函数是可变参数，也需要使用这种方式注册。IoC的内部实现不允许构造函数是可变参数。
            //container.RegisterInstance<ISaveTriageInfo>(new SaveTriageInfo1());

            //这样注册，会将上面的注册给替换掉，SaveTriageInfo1被替换成SaveTriageInfo2。
            //container.Register<ISaveTriageInfo, SaveTriageInfo2>();


            //高级注册说明，EcisContainer是其它IoC框架的封装
            //如果以上注册方法无法满足需求（这种情况应该很少遇到）
            //目前内部使用的是SimpleInjector，可查看其文档：https://simpleinjector.readthedocs.io/en/latest/quickstart.html
            //内部容器在：
            //container.Container
        }

        /// <summary>该插件所属分类，中英文均可，仅用于维护界面上的分类。</summary>
        public override string Module { get; } = "Demo";

        /// <summary>插件名称，建议英文名称，并与其它插件互斥（不强制要求），名称请勿太长</summary>
        public override string Name { get; } = "Demo_RegisterIoC";

        /// <summary>简单说明，建议中文说明，尽量简短</summary>
        public override string Caption { get; } = "注册IoC";

        /// <summary>详细说明，长度不限，应该尽量说明该插件的具体作用。</summary>
        public override string Description { get; } = "IoC插件，演示如何注册接口，无实际功能";

        /// <summary>详细说明图片，如果是涉及到界面上的插件，建议截图说明该插件的作用。</summary>
        public override Image DescriptionImage { get; } = null;

        /// <summary>插件配置项，用于插件数据的加载及保存</summary>
        public override List<PluginConfig> PluginConfigs { get; set; }

        #endregion
    }
}
