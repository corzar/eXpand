﻿using System;
using eXpand.ExpressApp.WorldCreator.PersistentTypesHelpers;
using eXpand.Persistent.Base.PersistentMetaData;

namespace eXpand.ExpressApp.WorldCreator.Core {
    public static class TypeInfoExtensions {
        public static void Init(this IPersistentTemplatedTypeInfo persistentTemplatedTypeInfo, Type codeTemplateType) {
            persistentTemplatedTypeInfo.CodeTemplateInfo = (ICodeTemplateInfo)Activator.CreateInstance(TypesInfo.Instance.CodeTemplateInfoType,
                                                                                                persistentTemplatedTypeInfo.Session);
            if (persistentTemplatedTypeInfo is IPersistentMemberInfo)
            {
                var persistentMemberInfo = ((IPersistentMemberInfo)persistentTemplatedTypeInfo);
                persistentMemberInfo.Init(codeTemplateType,persistentMemberInfo.Owner.PersistentAssemblyInfo.CodeDomProvider);
            }
            else if (persistentTemplatedTypeInfo is IPersistentClassInfo) {
                var persistentClassInfo = ((IPersistentClassInfo) persistentTemplatedTypeInfo);
                persistentClassInfo.Init(codeTemplateType,persistentClassInfo.PersistentAssemblyInfo.CodeDomProvider);
            }
        }

        public static void Init(this IPersistentMemberInfo persistentMemberInfo, Type codeTemplateType, CodeDomProvider codeDomProvider)
        {
            persistentMemberInfo.CodeTemplateInfo.CodeTemplate =CodeTemplateBuilder.CreateDefaultTemplate(
                persistentMemberInfo is IPersistentCollectionMemberInfo
                    ? TemplateType.ReadOnlyMember
                    : TemplateType.ReadWriteMember, persistentMemberInfo.Session, codeTemplateType,codeDomProvider);
        }

        public static void Init(this IPersistentClassInfo persistentClassInfo, Type codeTemplateType, CodeDomProvider codeDomProvider) {
            
            persistentClassInfo.CodeTemplateInfo.CodeTemplate = CodeTemplateBuilder.CreateDefaultTemplate(TemplateType.Class, persistentClassInfo.Session, codeTemplateType, codeDomProvider);
        }
    }
}