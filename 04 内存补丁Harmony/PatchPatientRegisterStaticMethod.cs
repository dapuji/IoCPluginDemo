using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using FxEcis.T0200.Core.Core40.Triage;
using FxEcis.T6200.Strategy;
using Harmony;

namespace IoCPluginDemo._04_内存补丁Harmony
{
    //这里注释掉的属性，仅在调用 harmony.PatchAll(Assembly.GetExecutingAssembly()); 方法时才需要
    //[HarmonyPatch(typeof(PatientRegisterStrategy), "PatientRegister")]
    public class PatchPatientRegisterStaticMethod
    {
        //这里每个方法的详细说明，在 https://github.com/pardeike/Harmony/wiki/Patching 
        //这里是拦截静态方法的写法。

        
        //这个方法是在拦截方法之前的准备操作。
        //这个方法好像已不推荐使用，因为手动注册中，无法注册该方法。并且提供的API中，也未找到该方法相关的信息。
        //但是该方法还可以使用自动注册注册上。
        //如果需要修改IL代码，可使用Transpiler方法
        //[HarmonyPrepare]
        //public static bool Prepare(HarmonyInstance instance)
        //{
        //    return true;
        //}

        /// <summary>
        ///     这个方法主要用于修改IL代码，比如说移除其中一段代码的执行。
        ///     这需要对IL代码有非常深入的了解，否则请勿使用该方法。
        /// </summary>
        /// <param name="original"></param>
        /// <param name="instructions"></param>
        /// <returns></returns>
        public static IEnumerable<CodeInstruction> Transpiler(MethodBase                   original,
                                                              IEnumerable<CodeInstruction> instructions)
        {
            return instructions;
        }

        /// <summary>
        ///     这个方法会在调用拦截的方法之前执行，这个方法的返回值需要是void或者bool，如果返回了false，则表示不再执行原方法
        /// </summary>
        /// <param name="package">这里是原方法的参数</param>
        /// <returns></returns>
        //[HarmonyPrefix]
        public static bool Prefix(TriagePackage package)
        {
            //如果拦截的是实例方法，而不是静态方法，则方法的参数可以增加 实例的类型 __instance这样的参数。这样就可以访问得到实例对象。
            return true;
        }

        /// <summary>
        ///     这个方法会在调用拦截的方法之后执行。
        /// </summary>
        /// <param name="__result">这个表示拦截的方法的实际的返回值，名字只能是这个</param>
        /// <param name="package">这里是原方法的参数</param>
        /// <returns></returns>
        //[HarmonyPostfix]
        public static bool Postfix(bool __result, TriagePackage package)
        {
            if (__result)
            {
                var stack  = new StackTrace();
                var frames = stack.GetFrames();

                if (frames.ToList().Exists(f=>f.GetMethod().Name == "ReRegisterbtn_Click"))
                {
                    UIRepository.PrintStrategy.PrintTriage(package.PatientVisit.PVID.ToString());
                }
            }
            return __result;
        }
        
    }
}