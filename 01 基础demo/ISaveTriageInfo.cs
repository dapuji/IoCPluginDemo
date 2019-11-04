using FxEcis.T0200.Core.Core40.Triage;

namespace IoCPluginDemo._01_基础demo
{
    /// <summary>
    ///     该接口只是模拟业务，不提供具体功能
    /// </summary>
    interface ISaveTriageInfo
    {
        /// <summary>
        ///     模拟保存分诊信息
        /// </summary>
        /// <param name="package"></param>
        string Save(TriagePackage package);
    }

    /// <summary>
    ///     假设这个是默认的实现（产品自带的），放在主程序内的。这里为了方便说明，放在一起。
    /// </summary>
    public class SaveTriageInfo1 : ISaveTriageInfo
    {
        /// <summary>
        ///     默认实现内的方法，如果是可能被改变的，比如说这种在分诊前验证的。可以设置成虚方法。
        ///     这样如果有后续扩展，可以直接继承SaveTriageInfo1这个类，然后仅仅继承这个方法，这样可以减少很多的工作量。
        /// </summary>
        /// <param name="package"></param>
        /// <returns></returns>
        protected virtual bool VerifyBeforeSave(TriagePackage package)
        {
            return true;
        }

        /// <summary>
        ///     这个方法可以设置成虚方法。
        /// </summary>
        /// <param name="package"></param>
        /// <returns></returns>
        public virtual string Save(TriagePackage package)
        {
            if (!VerifyBeforeSave(package))
            {
                return "SaveTriageInfo1：验证失败";
            }

            return "SaveTriageInfo1：使用第一个实现来保存分诊信息";
        }
    }

    /// <summary>
    ///     假设这个是根据现场的需求修改的功能，放在单独的DLL中。这里为了方便说明，放在一起。
    /// </summary>
    public class SaveTriageInfo2 : ISaveTriageInfo
    {
        //如果使用IoC，并且注册了ILogger，这里会自动解析并传参
        public SaveTriageInfo2(ILogger logger)
        {

        }

        public string Save(TriagePackage package)
        {
            return "SaveTriageInfo2：使用第二个实现来保存分诊信息";
        }
    }

    public interface ILogger
    {

    }

    public class Logger : ILogger
    {

    }
}
