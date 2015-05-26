using System;
using Android.App;
using Android.OS;
using Android.Util;
using Dot42;
using Dot42.Manifest;
using Newtonsoft.Json.Tests.Serialization;
using Newtonsoft.Json.Tests.Utilities;

[assembly: Application("Newtonsoft.Json.Tests.Dot42")]

namespace Newtonsoft.Json.Tests.Dot42
{
    [Activity]
    public class MainActivity : Activity
    {
        private readonly MyTestRunner _myTestRunner = new MyTestRunner("json");

        protected override void OnCreate(Bundle savedInstance)
        {
            base.OnCreate(savedInstance);
            SetContentView(R.Layout.MainLayout);

            new JsonConvertTest().CustomDoubleRounding();
            //RunTests();
        }

        //[Include]
        //protected override void OnResume()
        //{
        //    base.OnResume();

        //    Task.Run(RunTests);
        //}

        [Include]
        public void RunTests()
        {
            try
            {
                _myTestRunner.RunTests(new StringUtilsTests().GetType());
                _myTestRunner.RunTests(new LateboundReflectionDelegateFactoryTests().GetType());
                _myTestRunner.RunTests(new DateTimeUtilsTests().GetType());
                _myTestRunner.RunTests(new JsonConvertTest().GetType());
                _myTestRunner.RunTests(new JsonTextReaderTest().GetType());
                _myTestRunner.RunTests(new JsonTextWriterTest().GetType());
                _myTestRunner.RunTests(new JsonArrayAttributeTests().GetType());
                _myTestRunner.RunTests(new CamelCasePropertyNamesContractResolverTests().GetType());
                _myTestRunner.RunTests(new ConstructorHandlingTests().GetType());
                _myTestRunner.RunTests(new ContractResolverTests().GetType());
                _myTestRunner.RunTests(new JsonPropertyCollectionTests().GetType());
                _myTestRunner.RunTests(new JsonSerializerCollectionsTests().GetType());
                _myTestRunner.RunTests(new JsonSerializerTest().GetType());
                _myTestRunner.RunTests(new MetadataPropertyHandlingTests().GetType());
                _myTestRunner.RunTests(new MissingMemberHandlingTests().GetType());
                _myTestRunner.RunTests(new NullValueHandlingTests().GetType());
                _myTestRunner.RunTests(new PopulateTests().GetType());
                _myTestRunner.RunTests(new PreserveReferencesHandlingTests().GetType());
                _myTestRunner.RunTests(new ReferenceLoopHandlingTests().GetType());
                _myTestRunner.RunTests(new ReflectionAttributeProviderTests().GetType());
                _myTestRunner.RunTests(new SerializationErrorHandlingTests().GetType());
                _myTestRunner.RunTests(new SerializationEventAttributeTests().GetType());
                _myTestRunner.RunTests(new ShouldSerializeTests().GetType());
                _myTestRunner.RunTests(new TraceWriterTests().GetType());
                _myTestRunner.RunTests(new WebApiIntegrationTests().GetType());

            }
            catch (Exception ex)
            {
                Log.E("json-net", string.Format("fatal error"), ex);
            }
            catch 
            {
                Log.E("json-net", string.Format("fatal error with unkown exception type"));
            }
        }
    }
}
