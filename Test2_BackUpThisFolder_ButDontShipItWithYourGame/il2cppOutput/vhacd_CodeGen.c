#include "pch-c.h"
#ifndef _MSC_VER
# include <alloca.h>
#else
# include <malloc.h>
#endif


#include "codegen/il2cpp-codegen-metadata.h"





// 0x00000001 System.Void Grabbit.VHACD.VhacdGenerator::.ctor()
extern void VhacdGenerator__ctor_m3F25EE6DB2A0A6E73A44FD93314DC9F8BB76C4E3 (void);
// 0x00000002 System.Void* Grabbit.VHACD.VhacdGenerator::CreateVHACD()
extern void VhacdGenerator_CreateVHACD_m7AFBD851F7D4ADBC55DBFDB5848042B3D4F7E2B8 (void);
// 0x00000003 System.Void Grabbit.VHACD.VhacdGenerator::DestroyVHACD(System.Void*)
extern void VhacdGenerator_DestroyVHACD_m821B3A97B5FF95C31E0918860313C5FFD28840AF (void);
// 0x00000004 System.Boolean Grabbit.VHACD.VhacdGenerator::ComputeFloat(System.Void*,System.Single*,System.UInt32,System.UInt32*,System.UInt32,Grabbit.VHACD.VhacdGenerator/Parameters*)
extern void VhacdGenerator_ComputeFloat_m4DB19AC597F3EABEB3B26051AE1381FA24DC89B9 (void);
// 0x00000005 System.Boolean Grabbit.VHACD.VhacdGenerator::ComputeDouble(System.Void*,System.Double*,System.UInt32,System.UInt32*,System.UInt32,Grabbit.VHACD.VhacdGenerator/Parameters*)
extern void VhacdGenerator_ComputeDouble_mDCCD4A9C8AF16E5D4FEF9389BDEDA00B019B7A78 (void);
// 0x00000006 System.UInt32 Grabbit.VHACD.VhacdGenerator::GetNConvexHulls(System.Void*)
extern void VhacdGenerator_GetNConvexHulls_m5401676B38E4792F966C5488415388991243E754 (void);
// 0x00000007 System.Void Grabbit.VHACD.VhacdGenerator::GetConvexHull(System.Void*,System.UInt32,Grabbit.VHACD.VhacdGenerator/ConvexHull*)
extern void VhacdGenerator_GetConvexHull_m2E991B538AAC3E5F056A54472B98217FB61882EE (void);
// 0x00000008 System.Collections.Generic.List`1<UnityEngine.Mesh> Grabbit.VHACD.VhacdGenerator::GenerateConvexMeshes(UnityEngine.Mesh)
extern void VhacdGenerator_GenerateConvexMeshes_m43B9CC99D5CDFF1972B5E0F7A26419A2E2AB9E56 (void);
// 0x00000009 System.Void Grabbit.VHACD.VhacdGenerator::GenerateConvexMeshesThreaded(UnityEngine.Vector3[],System.Int32[])
extern void VhacdGenerator_GenerateConvexMeshesThreaded_mA0092F1C07A18EB5ED5E07A7295E0593AD4BFE8E (void);
// 0x0000000A System.Void Grabbit.VHACD.VhacdGenerator::ThreadedGenerateConvexMeshes(UnityEngine.Mesh)
extern void VhacdGenerator_ThreadedGenerateConvexMeshes_m168263B35904CDA835995C7F941B2E257490BFA9 (void);
// 0x0000000B System.Boolean Grabbit.VHACD.VhacdGenerator::IsThreadedMeshGenerationDone()
extern void VhacdGenerator_IsThreadedMeshGenerationDone_m2963FAFC0BF789EA72DFBCBA01EE104C31824FCA (void);
// 0x0000000C System.Void Grabbit.VHACD.VhacdGenerator::AbortThread()
extern void VhacdGenerator_AbortThread_mD8748D14F7AD4F1E812847843BE7F3A5E71879CF (void);
// 0x0000000D System.Collections.Generic.List`1<UnityEngine.Mesh> Grabbit.VHACD.VhacdGenerator::RetrieveThreadMesh()
extern void VhacdGenerator_RetrieveThreadMesh_mE270CC30AC8F19058FC1BCC89D4A8BD2BCFE870E (void);
// 0x0000000E System.Void Grabbit.VHACD.VhacdGenerator::FindAndUpdateMeshesInThread()
extern void VhacdGenerator_FindAndUpdateMeshesInThread_mF061ED6A34FAFA0FB28DAA2F8043AD923EA8EDA6 (void);
// 0x0000000F System.Void Grabbit.VHACD.VhacdGenerator::GenerateMeshesAfterThreadCompletion()
extern void VhacdGenerator_GenerateMeshesAfterThreadCompletion_mC5DE20DB977387407420C4AAAA58A3ECAE3854EE (void);
// 0x00000010 System.Void Grabbit.VHACD.VhacdGenerator/Parameters::Init()
extern void Parameters_Init_mA69348E6EB6C9FDD06350302FCD55B811362CDE3 (void);
static Il2CppMethodPointer s_methodPointers[16] = 
{
	VhacdGenerator__ctor_m3F25EE6DB2A0A6E73A44FD93314DC9F8BB76C4E3,
	VhacdGenerator_CreateVHACD_m7AFBD851F7D4ADBC55DBFDB5848042B3D4F7E2B8,
	VhacdGenerator_DestroyVHACD_m821B3A97B5FF95C31E0918860313C5FFD28840AF,
	VhacdGenerator_ComputeFloat_m4DB19AC597F3EABEB3B26051AE1381FA24DC89B9,
	VhacdGenerator_ComputeDouble_mDCCD4A9C8AF16E5D4FEF9389BDEDA00B019B7A78,
	VhacdGenerator_GetNConvexHulls_m5401676B38E4792F966C5488415388991243E754,
	VhacdGenerator_GetConvexHull_m2E991B538AAC3E5F056A54472B98217FB61882EE,
	VhacdGenerator_GenerateConvexMeshes_m43B9CC99D5CDFF1972B5E0F7A26419A2E2AB9E56,
	VhacdGenerator_GenerateConvexMeshesThreaded_mA0092F1C07A18EB5ED5E07A7295E0593AD4BFE8E,
	VhacdGenerator_ThreadedGenerateConvexMeshes_m168263B35904CDA835995C7F941B2E257490BFA9,
	VhacdGenerator_IsThreadedMeshGenerationDone_m2963FAFC0BF789EA72DFBCBA01EE104C31824FCA,
	VhacdGenerator_AbortThread_mD8748D14F7AD4F1E812847843BE7F3A5E71879CF,
	VhacdGenerator_RetrieveThreadMesh_mE270CC30AC8F19058FC1BCC89D4A8BD2BCFE870E,
	VhacdGenerator_FindAndUpdateMeshesInThread_mF061ED6A34FAFA0FB28DAA2F8043AD923EA8EDA6,
	VhacdGenerator_GenerateMeshesAfterThreadCompletion_mC5DE20DB977387407420C4AAAA58A3ECAE3854EE,
	Parameters_Init_mA69348E6EB6C9FDD06350302FCD55B811362CDE3,
};
extern void Parameters_Init_mA69348E6EB6C9FDD06350302FCD55B811362CDE3_AdjustorThunk (void);
static Il2CppTokenAdjustorThunkPair s_adjustorThunks[1] = 
{
	{ 0x06000010, Parameters_Init_mA69348E6EB6C9FDD06350302FCD55B811362CDE3_AdjustorThunk },
};
static const int32_t s_InvokerIndices[16] = 
{
	3544,
	5391,
	5318,
	3693,
	3693,
	5277,
	4471,
	2530,
	1586,
	2877,
	3390,
	3544,
	3451,
	3544,
	3544,
	3544,
};
IL2CPP_EXTERN_C const Il2CppCodeGenModule g_vhacd_CodeGenModule;
const Il2CppCodeGenModule g_vhacd_CodeGenModule = 
{
	"vhacd.dll",
	16,
	s_methodPointers,
	1,
	s_adjustorThunks,
	s_InvokerIndices,
	0,
	NULL,
	0,
	NULL,
	0,
	NULL,
	NULL,
	NULL, // module initializer,
	NULL,
	NULL,
	NULL,
};
