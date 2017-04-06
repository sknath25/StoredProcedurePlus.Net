using StoredProcedurePlus.Net.ErrorManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace StoredProcedurePlus.Net.EntityManagers.Factories
{
    internal class EntityInstanceFactory 
    {
        private delegate object ObjectActivator();
        private static ObjectActivator CreateCtor(Type type)
        {
            ConstructorInfo emptyConstructor = type.GetConstructor(Type.EmptyTypes);
            var dynamicMethod = new DynamicMethod("CreateInstance", type, Type.EmptyTypes, true);
            ILGenerator ilGenerator = dynamicMethod.GetILGenerator();
            ilGenerator.Emit(OpCodes.Nop);
            ilGenerator.Emit(OpCodes.Newobj, emptyConstructor);
            ilGenerator.Emit(OpCodes.Ret);
            return (ObjectActivator)dynamicMethod.CreateDelegate(typeof(ObjectActivator));
        }


        readonly ObjectActivator CtorPointer;
        protected EntityInstanceFactory(Type type)
        {
            CtorPointer = CreateCtor(type);
        }

        internal object CreateNewDefaultInstance()
        {
            return CtorPointer.Invoke();
        }
    }
}
