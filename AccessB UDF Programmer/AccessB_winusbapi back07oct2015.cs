/*******************************************************************************
Copyright 2015 Omar Andrés Trevizo Rascón

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*******************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using System.Windows.Forms;


//Mucahs delegate estas declaraciones SEHException obtuvieron de PINVOKE.NET


namespace AccessB_UDF_Programmer
{
    public class winusbapi
    {

        //ALGUNAS DE LAS DECLARACIONES DE PINVOKE, CONSTANTES Y ESTRUCTURAS FUERON CONSULTADAS EN PINVOKE.NET

        #region Constantes

        //Used in SetupDiClassDevs to get devices present in the system
        public const int DIGCF_PRESENT = 0x02;
        //Used in SetupDiClassDevs to get device interface details
        public const int DIGCF_DEVICEINTERFACE = 0x10;
        /// <summary>CreateFile : Open file for read</summary>
        public const uint GENERIC_READ = 0x80000000;
        /// <summary>CreateFile : Open file for write</summary>
        public const uint GENERIC_WRITE = 0x40000000;
        /// <summary>CreateFile : Open handle for overlapped operations</summary>
        public const uint FILE_FLAG_OVERLAPPED = 0x40000000;
        /// <summary>CreateFile : Resource to be "created" must exist</summary>
        public const uint OPEN_EXISTING = 3;
        //CreateFile: Enables subsequent open operations on a file or device to request read access.
        public const uint FILE_SHARE_READ = 0x00000001;
        //CreateFile: Enables subsequent open operations on a file or device to request write access.
        public const uint FILE_SHARE_WRITE = 0x00000002;
        //ERROR_IO_PENDING: Indica que una transferencia de datos asincrona aun no termina de realizarce.
        public const string ERROR_IO_PENDING = "997";
        //ERROR_PARAMETER_INCORRECT
        public const string ERROR_INVALID_PARAMETER = "87";
        //FILE_ATTRIBUTE_NORMAL
        public const uint FILE_ATTRIBUTE_NORMAL = 0x80;

        #endregion

        #region Estructuras

        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct Overlapped //Overlapped
        {
            private IntPtr InternalLow;
            private IntPtr InternalHigh;
            public long Offset;
            public IntPtr EventHandle;
        }
       
        [StructLayout(LayoutKind.Sequential)]
        public struct SP_DEVINFO_DATA
        {
            public uint cbSize;  //ANTES ERA uint
            public Guid ClassGuid;
            public uint DevInst;
            public IntPtr Reserved;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct SP_DEVICE_INTERFACE_DATA
        {
            public int cbSize;  //ANTES ERA uint
            public Guid InterfaceClassGuid;
            public int Flags;
            public int Reserved;
        }

        // Device interface detail data
        [StructLayout(LayoutKind.Sequential, Pack = 1)]//CharSet = CharSet.Auto)]
        public struct SP_DEVICE_INTERFACE_DETAIL_DATA
        {
            public int cbSize;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string DevicePath;
        }

        #endregion

        #region Declaración de estructuras

        //Estructura que contendrá información de la tarjeta
        private struct BrdProperties
        {
            public string DevicePath;
            public SafeFileHandle ReadHandle;
            public SafeFileHandle WriteHandle;
        }

        //ESTRUCTURA ANIDADA DENTRO DE LA CLASE PARA MANEJO INTERNO DE INFROMACION DE LA TARJETA.
        BrdProperties USBDeviceInfo = new BrdProperties();

        #endregion

        #region VID y PID de la tarjeta
        //LAS SIGUIENTES PROPIEDADES SON EL VID Y EL PID DE LA TARJETA, HAY VALORES POR DEFAULT PERO SE PUEDEN
        //CAMBIAR SEGUN SE OCUPE
        private int _VID = 0x04D8;
        public int VID
        {
            get
            {
                return _VID;
            }
            set
            {
                _VID = value;
            }
        }

        private int _PID = 0x003F;
        public int PID
        {
            get
            {
                return _PID;
            }
            set
            {
                _PID = value;
            }
        }

        #endregion

        #region API import DLLimports

        //Funcion que obtiene el GUID
        [DllImport("hid.dll", SetLastError = true)]
        public static extern void HidD_GetHidGuid(out Guid hidGuid);


        //Funcion que consigue la lista de dispositivos conectados y los da en un infoset
        [DllImport("setupapi.dll", SetLastError = true)]
        public static extern IntPtr SetupDiGetClassDevs(
                                                      ref Guid ClassGuid,
                                                      [MarshalAs(UnmanagedType.LPTStr)] string Enumerator,
                                                      IntPtr hwndParent,
                                                      uint Flags
                                                     );

        //Funcion que consigue la información de un dispositivo de la lista infoset creada por el SetupDiGetClassDevs
        [DllImport(@"setupapi.dll", SetLastError = true)]
        public static extern bool SetupDiEnumDeviceInterfaces(
                                                                    IntPtr hDevInfo,                //Infoset de dispositivos
                                                                    uint devInfo,//ref SP_DEVINFO_DATA devInfo,   
                                                                    ref Guid interfaceClassGuid,    //GUID obtenido con HidD_GetHidGuid
                                                                    uint memberIndex,             //Index que apunta a la lista 0-n
                                                                    ref SP_DEVICE_INTERFACE_DATA deviceInterfaceData //Variable declarada anteriormente
                                                                    );

        //SetupDiGetDeviceInterfaceDetal, el pinvoke esta sobrecargado, se usa la funcion dos veces con argumentos distintos
        [DllImport(@"setupapi.dll", /*CharSet = CharSet.Auto,*/ SetLastError = true)]
        public static extern Boolean SetupDiGetDeviceInterfaceDetail(
                                                                        IntPtr hDevInfo,
                                                                        ref SP_DEVICE_INTERFACE_DATA deviceInterfaceData,
                                                                        ref SP_DEVICE_INTERFACE_DETAIL_DATA deviceInterfaceDetailData,
                                                                        uint deviceInterfaceDetailDataSize,
                                                                        ref uint requiredSize,//out UInt32 requiredSize,
                                                                        IntPtr deviceInfoData//ref SP_DEVINFO_DATA deviceInfoData
                                                                        );

        [DllImport("setupapi.dll", SetLastError = true)]
        public static extern bool SetupDiGetDeviceInterfaceDetail(
                                                                        IntPtr lpDeviceInfoSet,
                                                                        ref SP_DEVICE_INTERFACE_DATA oInterfaceData,
                                                                        IntPtr lpDeviceInterfaceDetailData,
                                                                        uint nDeviceInterfaceDetailDataSize,
                                                                        ref uint nRequiredSize,
                                                                        IntPtr lpDeviceInfoData
                                                                        );


        [DllImport("setupapi.dll", SetLastError = true)]
        public static extern bool SetupDiDestroyDeviceInfoList
        (
             IntPtr DeviceInfoSet
        );

        //Esta funcion me permite abrir archivos, puertos com, puertos lpt, dispositivos usb etc. Se usa SafeFileHandle en lugar de IntPrt, por lo que 
        //ya no se requiere de closehandle sino solo usar el metodo handle.close() o handle.Dispose()
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern SafeFileHandle CreateFile( 
            [MarshalAs(UnmanagedType.LPStr)] 
            string strName, 
            uint nAccess, 
            uint nShareMode, 
            IntPtr lpSecurity, 
            uint nCreationFlags, 
            uint nAttributes, 
            IntPtr lpTemplate);
        
        //Con esta funcion leo los datos recibidos y lo paso a un buffer donde puedo usarlo para mis aplicaciones
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool ReadFile
        (
            SafeFileHandle hFile,      // SafeFileHandle to file
            IntPtr pBuffer,            // data buffer
            int NumberOfBytesToRead,  // number of bytes to read
            ref Int32 pNumberOfBytesRead,  // number of bytes read
            IntPtr OverlappedBuffer//IntPtr OverlappedBuffer            // overlapped buffer
        );

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool WriteFile
        (
            SafeFileHandle hFile, //Manejador del archivo
            byte[] pBuffer,  //Buffer de datos a enviar
            int NumberOfBytesToRead, //Numero de bytes a transmitir
            ref Int32 NumbersOfBytesWritten,//IntPtr NumbersOfBytesWritten, //aun no se para que es este parametro
            IntPtr OverlappedBuffer  //Aun no se para que es este parametro
        );

        //Esta funcion sirve para ver si ya se termino una transferencia asincrona u Overlapped
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool GetOverlappedResult(
            SafeFileHandle hFile,//IntPtr hFile, //Puntero hacia el manejador del archivo creado
            [In] ref Overlapped lpOverlapped, //[In] ref System.Threading.NativeOverlapped lpOverlapped, //Puntero a la estructura overlapped que se creo con readfile
            ref long lpNumberofBytesTransferred,//IntPtr lpNumberofBytesTransferred,//out uint lpNumberofBytesTransferred, //Vriable donde se van a recibir los datos
            bool bWait //Si esta variable es true, la funcion no regresara hasta que se haya terminado la transferencia asincrona
            );

        //Esta funcion sirve para cancelar transferencias de datos
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CancelIo(
            SafeFileHandle hFile
            );

        #endregion

        #region Metodos Base para comunciación USB

        //Metodo de ayuda para encontrar la ruta al dispositivo que se busca, esta funcion es llamada desde el metodo "EncuentraDevHID"
        private string GetDevicePath(IntPtr Infoset, ref SP_DEVICE_INTERFACE_DATA DeviceInterfaceData)
        {
            uint nRequiredSize = 0;
            //Para obtener la ruta del dispositivo se hace un proceso de dos pasos, llamar a SetupDiGetDeviceInterfaceDetail la primer vez para obtener el espacio a guardar en cSize
            //Llamar por segunda vez a la misma funcion para obtener la ruta del dispositivo.
            if (!SetupDiGetDeviceInterfaceDetail(Infoset, ref DeviceInterfaceData, IntPtr.Zero, 0, ref nRequiredSize, IntPtr.Zero))//Primera llamada
            {
                SP_DEVICE_INTERFACE_DETAIL_DATA dDetail = new SP_DEVICE_INTERFACE_DETAIL_DATA();

                if (IntPtr.Size == 8) // for 64 bit operating systems
                {
                    dDetail.cbSize = 8;
                }
                else
                {
                    dDetail.cbSize = 5; // for 32 bit systems
                }

                if (SetupDiGetDeviceInterfaceDetail(Infoset, ref DeviceInterfaceData, ref dDetail, nRequiredSize, ref nRequiredSize, IntPtr.Zero)) //Segunda llamada
                {
                    return dDetail.DevicePath;//Listo se encontro la ruta de algun dispositivo, falta ahora ver si coinciden VIP y PID
                }
                string error = Marshal.GetLastWin32Error().ToString();
            }
            return null; //No se encontro ningun otro dispositivo en la lista :(
        }

        //Este metodo de la clase USBboard18F encuentra la tarjeta y crea los handles de lectura y escritura de datos
        //Regresa true si encuentra la tarjeta y logro crear los handles, false si no encontró tarjeta o logró crear los handles
        public bool FindDevHID()
        {
            Guid ghid;
            IntPtr USBBoardinfoset;

            //VID y PID
            string strSearch = string.Format("vid_{0:x4}&pid_{1:x4}", VID, PID);
            HidD_GetHidGuid(out ghid);

            //Obtengo la lista de dispositivos y los guardo en un infoset
            USBBoardinfoset = SetupDiGetClassDevs(ref ghid, null, IntPtr.Zero, DIGCF_DEVICEINTERFACE | DIGCF_PRESENT);

            //Las siguientes dos lineas de codigo las aconsejan en pinvoke.net...
            SP_DEVICE_INTERFACE_DATA dInterface = new SP_DEVICE_INTERFACE_DATA();

            //Lo siguiente es buscar nuestro dispositivo en la lista que se obtuvo con un ciclo loop, para sistemas de
            //64bits
            int index = 0;
            dInterface.cbSize = 32;
            while (SetupDiEnumDeviceInterfaces(USBBoardinfoset, 0, ref ghid, (uint)index, ref dInterface))
            {
                USBDeviceInfo.DevicePath = GetDevicePath(USBBoardinfoset, ref dInterface);
                if (USBDeviceInfo.DevicePath.IndexOf(strSearch) >= 0)
                {
                    //Lectura sincrona
                    USBDeviceInfo.ReadHandle = CreateFile(USBDeviceInfo.DevicePath, GENERIC_READ, FILE_SHARE_READ | FILE_SHARE_WRITE, IntPtr.Zero, OPEN_EXISTING, 0, IntPtr.Zero);
                    USBDeviceInfo.WriteHandle = CreateFile(USBDeviceInfo.DevicePath, GENERIC_WRITE, FILE_SHARE_READ | FILE_SHARE_WRITE, IntPtr.Zero, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, IntPtr.Zero);
                    return true; //Exito
                }

                index++; //Prueba con el siguiente de la lista
            }

            //En caso de que el sistema sea de 32 bits se vuelve a hacer la busqueda del dispositivo
            index = 0;
            dInterface.cbSize = 28;
            while (SetupDiEnumDeviceInterfaces(USBBoardinfoset, 0, ref ghid, (uint)index, ref dInterface))
            {
                USBDeviceInfo.DevicePath = GetDevicePath(USBBoardinfoset, ref dInterface);
                if (USBDeviceInfo.DevicePath.IndexOf(strSearch) >= 0)
                {
                    //Lectura sincrona
                    USBDeviceInfo.ReadHandle = CreateFile(USBDeviceInfo.DevicePath, GENERIC_READ, FILE_SHARE_READ | FILE_SHARE_WRITE, IntPtr.Zero, OPEN_EXISTING, 0, IntPtr.Zero);
                    USBDeviceInfo.WriteHandle = CreateFile(USBDeviceInfo.DevicePath, GENERIC_WRITE, FILE_SHARE_READ | FILE_SHARE_WRITE, IntPtr.Zero, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, IntPtr.Zero);
                    return true; //Exito
                }

                index++; //Prueba con el siguiente de la lista

            }

            string error = Marshal.GetLastWin32Error().ToString();
            return false; //No se encontro dispositivo
        }

        //Este metodo realiza lectura de los datos desde la tarjeta de forma sincrona, por lo que es una funcion bloqueante, se puede hacer que la funcion use un thread si se pasa el parametro thread como true
        /// <summary>
        /// Read RAW data from device, synch read
        /// </summary>
        /// <param name="InBuffer">65 bytes long array, the first byte will be ALWAYS zero and can be discarded, the next 64 bytes is the RAW data from the device.</param>
        /// <param name="thread">I'm working on it, for now set it false or it wont work.</param>
        /// <returns></returns>
        public bool ReadUSB(ref byte[] InBuffer, bool thread)
        {
            int Bsize = 65; //Cantidad de bytes a leer, deberia de ser 64 pero son 65 posiblemente el problema esta en el marshal.copy al final, 64 espesificado por el CUSTOM HID demo PIC18F2550
            InBuffer = new byte[Bsize];
            Int32 BytesRead = 0;

            //Con allochglobal creo un buffer del tamaño espesificado y nos proporciona un puntero hacia el mismo
            IntPtr UnManagedBuffer = Marshal.AllocHGlobal(Bsize); //Crea un buffer del tamaño espesificado y regresa el puntero hacia ese buffer

            switch (thread)
            {
                case (false)://Lectura sincrona
                    {
                        if (ReadFile(USBDeviceInfo.ReadHandle, UnManagedBuffer, Bsize, ref BytesRead, IntPtr.Zero) == false)
                        {
                            MessageBox.Show("GetLastWin32Error: " + Marshal.GetLastWin32Error().ToString());
                            return false;
                        }
                    }
                    break;

                case (true)://Lectura sincrona pero en otro thread, para no bloquear la ejecución del prorama
                    {
                        //POR ESCRIBIR
                    }
                    break;
            }

            //Paso los datos leeidos del unmanaged buffer al managed buffer
            Marshal.Copy(UnManagedBuffer, InBuffer, 0, Bsize);//El ultimo argumento es el numero de bytes a copiar, no se por que solo me funciona con 3 cuando deberiand e ser 2
            //Libero recursos
            Marshal.FreeHGlobal(UnManagedBuffer);
            return true;
        }

        //Metodo para escribir hacia la tarjeta
        /// <summary>
        /// Send RAW data to the device, synch write
        /// </summary>
        /// <param name="OutBuffer">65 byte long array, NOTE: the first byte ALWAYS must be zero or it wont work, that first byte isn't received on the device side, the next 64 bytes are the RAW data to be send to the device</param>
        /// <returns>True is data transfer was success, false if not.</returns>
        public bool WriteUSB(ref byte[] OutBuffer)
        {
            int Bsize = OutBuffer.Length;
            string error;
            Int32 NumbersOfBytesWritten = 0;
            IntPtr UnManagedBuffer = Marshal.AllocHGlobal(Bsize); //Crea un buffer del tamaño espesificado y regresa el puntero hacia ese buffer
            Marshal.Copy(OutBuffer, 0, UnManagedBuffer, Bsize);//Paso los datos a transmitir a un buffer no manejado

            if (WriteFile(USBDeviceInfo.WriteHandle, OutBuffer, Bsize, ref NumbersOfBytesWritten, IntPtr.Zero) == false)
            {
                error = Marshal.GetLastWin32Error().ToString();
                return false;
            }

            //Libero recursos
            if (Marshal.GetLastWin32Error().ToString() == "0")
            {
                Marshal.FreeHGlobal(UnManagedBuffer);
                return true;
            }

            error = Marshal.GetLastWin32Error().ToString();
            return false;
        }

        //Metodo para limpiar el buffer de recepcion, cierra el pipe por un segundo y luego lo vuelve a abrir.
        public void ClearRxBuffer()
        {
            USBDeviceInfo.ReadHandle.Dispose();
            DateTime Delay = DateTime.Now.AddMilliseconds(100); //1 Sec. delay
            while (Delay > DateTime.Now) { }
            USBDeviceInfo.ReadHandle = CreateFile(USBDeviceInfo.DevicePath, GENERIC_READ, FILE_SHARE_READ | FILE_SHARE_WRITE, IntPtr.Zero, OPEN_EXISTING, 0, IntPtr.Zero);
        }

        #endregion

    }
}
