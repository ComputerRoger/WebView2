

/* this ALWAYS GENERATED file contains the definitions for the interfaces */


 /* File created by MIDL compiler version 8.01.0622 */
/* at Mon Jan 18 22:14:07 2038
 */
/* Compiler settings for RemoteComObject.idl:
    Oicf, W1, Zp8, env=Win32 (32b run), target_arch=X86 8.01.0622 
    protocol : dce , ms_ext, c_ext, robust
    error checks: allocation ref bounds_check enum stub_data 
    VC __declspec() decoration level: 
         __declspec(uuid()), __declspec(selectany), __declspec(novtable)
         DECLSPEC_UUID(), MIDL_INTERFACE()
*/
/* @@MIDL_FILE_HEADING(  ) */



/* verify that the <rpcndr.h> version is high enough to compile this file*/
#ifndef __REQUIRED_RPCNDR_H_VERSION__
#define __REQUIRED_RPCNDR_H_VERSION__ 500
#endif

#include "rpc.h"
#include "rpcndr.h"

#ifndef __RPCNDR_H_VERSION__
#error this stub requires an updated version of <rpcndr.h>
#endif /* __RPCNDR_H_VERSION__ */

#ifndef COM_NO_WINDOWS_H
#include "windows.h"
#include "ole2.h"
#endif /*COM_NO_WINDOWS_H*/

#ifndef __RemoteComObject_i_h__
#define __RemoteComObject_i_h__

#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

/* Forward Declarations */ 

#ifndef __IRemoteComObjectImpl_FWD_DEFINED__
#define __IRemoteComObjectImpl_FWD_DEFINED__
typedef interface IRemoteComObjectImpl IRemoteComObjectImpl;

#endif 	/* __IRemoteComObjectImpl_FWD_DEFINED__ */


#ifndef __RemoteComObjectImpl_FWD_DEFINED__
#define __RemoteComObjectImpl_FWD_DEFINED__

#ifdef __cplusplus
typedef class RemoteComObjectImpl RemoteComObjectImpl;
#else
typedef struct RemoteComObjectImpl RemoteComObjectImpl;
#endif /* __cplusplus */

#endif 	/* __RemoteComObjectImpl_FWD_DEFINED__ */


/* header files for imported files */
#include "oaidl.h"
#include "ocidl.h"

#ifdef __cplusplus
extern "C"{
#endif 


#ifndef __IRemoteComObjectImpl_INTERFACE_DEFINED__
#define __IRemoteComObjectImpl_INTERFACE_DEFINED__

/* interface IRemoteComObjectImpl */
/* [unique][nonextensible][dual][uuid][object] */ 


EXTERN_C const IID IID_IRemoteComObjectImpl;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("BC4C0BCE-56F6-45BF-A9D4-6E314C9D2A4D")
    IRemoteComObjectImpl : public IDispatch
    {
    public:
        virtual /* [id][propget] */ HRESULT STDMETHODCALLTYPE get_Property( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [id][propput] */ HRESULT STDMETHODCALLTYPE put_Property( 
            /* [in] */ BSTR newVal) = 0;
        
        virtual /* [id] */ HRESULT STDMETHODCALLTYPE MethodWithParametersAndReturnValue( 
            BSTR stringParameter,
            INT integerParameter,
            /* [retval][out] */ BSTR *stringResult) = 0;
        
        virtual /* [id] */ HRESULT STDMETHODCALLTYPE CallCallbackAsynchronously( 
            IDispatch *callbackParameter) = 0;
        
    };
    
    
#else 	/* C style interface */

    typedef struct IRemoteComObjectImplVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            IRemoteComObjectImpl * This,
            /* [in] */ REFIID riid,
            /* [annotation][iid_is][out] */ 
            _COM_Outptr_  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            IRemoteComObjectImpl * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            IRemoteComObjectImpl * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfoCount )( 
            IRemoteComObjectImpl * This,
            /* [out] */ UINT *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfo )( 
            IRemoteComObjectImpl * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo **ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetIDsOfNames )( 
            IRemoteComObjectImpl * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR *rgszNames,
            /* [range][in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE *Invoke )( 
            IRemoteComObjectImpl * This,
            /* [annotation][in] */ 
            _In_  DISPID dispIdMember,
            /* [annotation][in] */ 
            _In_  REFIID riid,
            /* [annotation][in] */ 
            _In_  LCID lcid,
            /* [annotation][in] */ 
            _In_  WORD wFlags,
            /* [annotation][out][in] */ 
            _In_  DISPPARAMS *pDispParams,
            /* [annotation][out] */ 
            _Out_opt_  VARIANT *pVarResult,
            /* [annotation][out] */ 
            _Out_opt_  EXCEPINFO *pExcepInfo,
            /* [annotation][out] */ 
            _Out_opt_  UINT *puArgErr);
        
        /* [id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_Property )( 
            IRemoteComObjectImpl * This,
            /* [retval][out] */ BSTR *pVal);
        
        /* [id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_Property )( 
            IRemoteComObjectImpl * This,
            /* [in] */ BSTR newVal);
        
        /* [id] */ HRESULT ( STDMETHODCALLTYPE *MethodWithParametersAndReturnValue )( 
            IRemoteComObjectImpl * This,
            BSTR stringParameter,
            INT integerParameter,
            /* [retval][out] */ BSTR *stringResult);
        
        /* [id] */ HRESULT ( STDMETHODCALLTYPE *CallCallbackAsynchronously )( 
            IRemoteComObjectImpl * This,
            IDispatch *callbackParameter);
        
        END_INTERFACE
    } IRemoteComObjectImplVtbl;

    interface IRemoteComObjectImpl
    {
        CONST_VTBL struct IRemoteComObjectImplVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IRemoteComObjectImpl_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define IRemoteComObjectImpl_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define IRemoteComObjectImpl_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define IRemoteComObjectImpl_GetTypeInfoCount(This,pctinfo)	\
    ( (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo) ) 

#define IRemoteComObjectImpl_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    ( (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo) ) 

#define IRemoteComObjectImpl_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    ( (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId) ) 

#define IRemoteComObjectImpl_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    ( (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr) ) 


#define IRemoteComObjectImpl_get_Property(This,pVal)	\
    ( (This)->lpVtbl -> get_Property(This,pVal) ) 

#define IRemoteComObjectImpl_put_Property(This,newVal)	\
    ( (This)->lpVtbl -> put_Property(This,newVal) ) 

#define IRemoteComObjectImpl_MethodWithParametersAndReturnValue(This,stringParameter,integerParameter,stringResult)	\
    ( (This)->lpVtbl -> MethodWithParametersAndReturnValue(This,stringParameter,integerParameter,stringResult) ) 

#define IRemoteComObjectImpl_CallCallbackAsynchronously(This,callbackParameter)	\
    ( (This)->lpVtbl -> CallCallbackAsynchronously(This,callbackParameter) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __IRemoteComObjectImpl_INTERFACE_DEFINED__ */



#ifndef __RemoteComObjectLib_LIBRARY_DEFINED__
#define __RemoteComObjectLib_LIBRARY_DEFINED__

/* library RemoteComObjectLib */
/* [version][uuid] */ 


EXTERN_C const IID LIBID_RemoteComObjectLib;

EXTERN_C const CLSID CLSID_RemoteComObjectImpl;

#ifdef __cplusplus

class DECLSPEC_UUID("3328FE46-65D4-431D-B356-E2FE2525C4B8")
RemoteComObjectImpl;
#endif
#endif /* __RemoteComObjectLib_LIBRARY_DEFINED__ */

/* Additional Prototypes for ALL interfaces */

unsigned long             __RPC_USER  BSTR_UserSize(     unsigned long *, unsigned long            , BSTR * ); 
unsigned char * __RPC_USER  BSTR_UserMarshal(  unsigned long *, unsigned char *, BSTR * ); 
unsigned char * __RPC_USER  BSTR_UserUnmarshal(unsigned long *, unsigned char *, BSTR * ); 
void                      __RPC_USER  BSTR_UserFree(     unsigned long *, BSTR * ); 

unsigned long             __RPC_USER  BSTR_UserSize64(     unsigned long *, unsigned long            , BSTR * ); 
unsigned char * __RPC_USER  BSTR_UserMarshal64(  unsigned long *, unsigned char *, BSTR * ); 
unsigned char * __RPC_USER  BSTR_UserUnmarshal64(unsigned long *, unsigned char *, BSTR * ); 
void                      __RPC_USER  BSTR_UserFree64(     unsigned long *, BSTR * ); 

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif


