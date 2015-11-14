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


namespace AccessB_UDF_Programmer
{
    public class AccessB : winusbapi
    {

        #region Comandos implementados en el Firmware del PIC18F2550

        public byte CMD_ReadSFR { get { return 0x01; } }
        public byte CMD_WriteSFR { get { return 0x02; } }
        public byte CMD_ReadGPR { get { return 0x03; } }
        public byte CMD_WriteGPR { get { return 0x04; } }
        public byte CMD_ADC_CFG { get { return 0x06; } }
        public byte CMD_ADC_Val { get { return 0x07; } }
        public byte CMD_ACOMP_CFG { get { return 0x08; } }
        public byte CMD_ACOMP_VALUE { get { return 0x09; } }
        public byte CMD_SPI_CFG { get { return 0x0A; } }
        public byte CMD_SPI_TRANSFERENCE { get { return 0x0C; } }
        public byte SFR_CHANGE_BIT_VALUE { get { return 0x0D; } }
        public byte CMD_I2C_CFG { get { return 0x0E; } }
        public byte CMD_I2C_TRANSFERENCE { get { return 0x10; } }
        public byte CMD_CCP_CFG { get { return 0x11; } }
        public byte CMD_PWM_FPWM { get { return 0x12; } }
        public byte CMD_PWM_DC { get { return 0x13; } }
        public byte CMD_EUSART_TX { get { return 0x14; } }
        public byte CMD_EUSART_RX { get { return 0x15; } }
        public byte CMD_READ_BIT_VALUE { get { return 0x16; } }
        public byte CMD_CALL_UDF { get { return 0x18; } }
        public byte CMD_PROGRAM_UDF { get { return 0x19; } }
        public byte UDF_PROGRAM_FAIL { get { return 0xFA; } }
        public byte UDF_PROGRAM_SUCCESS { get { return 0xFB; } }
        public byte UDF_READY { get { return 0xFF; } }
        public byte ADC_GO { get { return 0xFB; } }
        public byte ADC_STOP { get { return 0xFA; } }
        public byte CMD_TEST { get { return 0xFF; } }

        #endregion

        #region Regitros de proposito especial del PIC18F2550
        /* Dentro de esta región se encuentran las propiedades de la clase USBboard18F que definen los 
        *  SFR (Special File Registers) y su dirección dentro del PIC para un modelo 18F2550, la intención
        *  es de usar las propiedades en conjunto con los metodos de lectura y escritura al enviar
        *  comandos al PIC.
        */
        
        public UInt32 PORTA { set { SFRValue(128, value); } get { return SFRValue(128); } }
        public UInt32 PORTAbits_RA0 { set { SFRBitValue(128, 0, value); } get { return SFRBitValue(128, 0); } }
        public UInt32 PORTAbits_RA1 { set { SFRBitValue(128, 1, value); } get { return SFRBitValue(128, 1); } }
        public UInt32 PORTAbits_RA2 { set { SFRBitValue(128, 2, value); } get { return SFRBitValue(128, 2); } }
        public UInt32 PORTAbits_RA3 { set { SFRBitValue(128, 3, value); } get { return SFRBitValue(128, 3); } }
        public UInt32 PORTAbits_RA4 { set { SFRBitValue(128, 4, value); } get { return SFRBitValue(128, 4); } }
        public UInt32 PORTAbits_RA5 { set { SFRBitValue(128, 5, value); } get { return SFRBitValue(128, 5); } }
        public UInt32 PORTAbits_RA6 { set { SFRBitValue(128, 6, value); } get { return SFRBitValue(128, 6); } }

        public UInt32 PORTB { set { SFRValue(129, value); } get { return SFRValue(129); } }
        public UInt32 PORTBbits_RB0 { set { SFRBitValue(129, 0, value); } get { return SFRBitValue(129, 0); } }
        public UInt32 PORTBbits_RB1 { set { SFRBitValue(129, 1, value); } get { return SFRBitValue(129, 1); } }
        public UInt32 PORTBbits_RB2 { set { SFRBitValue(129, 2, value); } get { return SFRBitValue(129, 2); } }
        public UInt32 PORTBbits_RB3 { set { SFRBitValue(129, 3, value); } get { return SFRBitValue(129, 3); } }
        public UInt32 PORTBbits_RB4 { set { SFRBitValue(129, 4, value); } get { return SFRBitValue(129, 4); } }
        public UInt32 PORTBbits_RB5 { set { SFRBitValue(129, 5, value); } get { return SFRBitValue(129, 5); } }
        public UInt32 PORTBbits_RB6 { set { SFRBitValue(129, 6, value); } get { return SFRBitValue(129, 6); } }
        public UInt32 PORTBbits_RB7 { set { SFRBitValue(129, 7, value); } get { return SFRBitValue(129, 7); } }

        public UInt32 PORTC { set { SFRValue(130, value); } get { return SFRValue(130); } }
        public UInt32 PORTCbits_RC0 { set { SFRBitValue(130, 0 , value); } get { return SFRBitValue(130, 0); } }
        public UInt32 PORTCbits_RC1 { set { SFRBitValue(130, 1 , value); } get { return SFRBitValue(130, 1); } }
        public UInt32 PORTCbits_RC2 { set { SFRBitValue(130, 2 , value); } get { return SFRBitValue(130, 2); } }
        public UInt32 PORTCbits_RC6 { set { SFRBitValue(130, 6 , value); } get { return SFRBitValue(130, 6); } }
        public UInt32 PORTCbits_RC7 { set { SFRBitValue(130, 7 , value); } get { return SFRBitValue(130, 7); } }

        //Read only bit on 18F2550 devices, TRISE and LATE aren't implemented on 18F2550 devices
        public UInt32 PORTEbits_RE3 { get { return SFRBitValue(132, 3); } }

        public UInt32 LATA { set { SFRValue(137, value); } get { return SFRValue(137); } }
        public UInt32 LATAbits_LATA0 { set { SFRBitValue(137, 0, value); } get { return SFRBitValue(137, 0); } }
        public UInt32 LATAbits_LATA1 { set { SFRBitValue(137, 1, value); } get { return SFRBitValue(137, 1); } }
        public UInt32 LATAbits_LATA2 { set { SFRBitValue(137, 2, value); } get { return SFRBitValue(137, 2); } }
        public UInt32 LATAbits_LATA3 { set { SFRBitValue(137, 3, value); } get { return SFRBitValue(137, 3); } }
        public UInt32 LATAbits_LATA4 { set { SFRBitValue(137, 4, value); } get { return SFRBitValue(137, 4); } }
        public UInt32 LATAbits_LATA5 { set { SFRBitValue(137, 5, value); } get { return SFRBitValue(137, 5); } }
        public UInt32 LATAbits_LATA6 { set { SFRBitValue(137, 6, value); } get { return SFRBitValue(137, 6); } }

        public UInt32 LATB { set { SFRValue(138, value); } get { return SFRValue(138); } }
        public UInt32 LATBbits_LATB0 { set { SFRBitValue(138, 0, value); } get { return SFRBitValue(138, 0); } }
        public UInt32 LATBbits_LATB1 { set { SFRBitValue(138, 1, value); } get { return SFRBitValue(138, 1); } }
        public UInt32 LATBbits_LATB2 { set { SFRBitValue(138, 2, value); } get { return SFRBitValue(138, 2); } }
        public UInt32 LATBbits_LATB3 { set { SFRBitValue(138, 3, value); } get { return SFRBitValue(138, 3); } }
        public UInt32 LATBbits_LATB4 { set { SFRBitValue(138, 4, value); } get { return SFRBitValue(138, 4); } }
        public UInt32 LATBbits_LATB5 { set { SFRBitValue(138, 5, value); } get { return SFRBitValue(138, 5); } }
        public UInt32 LATBbits_LATB6 { set { SFRBitValue(138, 6, value); } get { return SFRBitValue(138, 6); } }
        public UInt32 LATBbits_LATB7 { set { SFRBitValue(138, 7, value); } get { return SFRBitValue(138, 7); } }

        public UInt32 LATC { set { SFRValue(139, value); } get { return SFRValue(139); } }
        public UInt32 LATCbits_LATC0 { set { SFRBitValue(139, 0 , value); } get { return SFRBitValue(139, 0); } }
        public UInt32 LATCbits_LATC1 { set { SFRBitValue(139, 1 , value); } get { return SFRBitValue(139, 1); } }
        public UInt32 LATCbits_LATC2 { set { SFRBitValue(139, 2 , value); } get { return SFRBitValue(139, 2); } }
        public UInt32 LATCbits_LATC6 { set { SFRBitValue(139, 6 , value); } get { return SFRBitValue(139, 6); } }
        public UInt32 LATCbits_LATC7 { set { SFRBitValue(139, 7 , value); } get { return SFRBitValue(139, 7); } }

        public UInt32 TRISA { set { SFRValue(146, value); } get { return SFRValue(146); } }
        public UInt32 TRISAbits_TRISA0 { set { SFRBitValue(146, 0, value); } get { return SFRBitValue(146, 0); } }
        public UInt32 TRISAbits_TRISA1 { set { SFRBitValue(146, 1, value); } get { return SFRBitValue(146, 1); } }
        public UInt32 TRISAbits_TRISA2 { set { SFRBitValue(146, 2, value); } get { return SFRBitValue(146, 2); } }
        public UInt32 TRISAbits_TRISA3 { set { SFRBitValue(146, 3, value); } get { return SFRBitValue(146, 3); } }
        public UInt32 TRISAbits_TRISA4 { set { SFRBitValue(146, 4, value); } get { return SFRBitValue(146, 4); } }
        public UInt32 TRISAbits_TRISA5 { set { SFRBitValue(146, 5, value); } get { return SFRBitValue(146, 5); } }
        public UInt32 TRISAbits_TRISA6 { set { SFRBitValue(146, 6, value); } get { return SFRBitValue(146, 6); } }

        public UInt32 TRISB { set { SFRValue(147, value); } get { return SFRValue(147); } }
        public UInt32 TRISBbits_TRISB0 { set { SFRBitValue(147, 0, value); } get { return SFRBitValue(147, 0); } }
        public UInt32 TRISBbits_TRISB1 { set { SFRBitValue(147, 1, value); } get { return SFRBitValue(147, 1); } }
        public UInt32 TRISBbits_TRISB2 { set { SFRBitValue(147, 2, value); } get { return SFRBitValue(147, 2); } }
        public UInt32 TRISBbits_TRISB3 { set { SFRBitValue(147, 3, value); } get { return SFRBitValue(147, 3); } }
        public UInt32 TRISBbits_TRISB4 { set { SFRBitValue(147, 4, value); } get { return SFRBitValue(147, 4); } }
        public UInt32 TRISBbits_TRISB5 { set { SFRBitValue(147, 5, value); } get { return SFRBitValue(147, 5); } }
        public UInt32 TRISBbits_TRISB6 { set { SFRBitValue(147, 6, value); } get { return SFRBitValue(147, 6); } }
        public UInt32 TRISBbits_TRISB7 { set { SFRBitValue(147, 7, value); } get { return SFRBitValue(147, 7); } }

        public UInt32 TRISC { set { SFRValue(148, value); } get { return SFRValue(148); } }
        public UInt32 TRISCbits_TRISC0 { set { SFRBitValue(148, 0 , value); } get { return SFRBitValue(148, 0); } }
        public UInt32 TRISCbits_TRISC1 { set { SFRBitValue(148, 1 , value); } get { return SFRBitValue(148, 1); } }
        public UInt32 TRISCbits_TRISC2 { set { SFRBitValue(148, 2 , value); } get { return SFRBitValue(148, 2); } }
        public UInt32 TRISCbits_TRISC6 { set { SFRBitValue(148, 6 , value); } get { return SFRBitValue(148, 6); } }
        public UInt32 TRISCbits_TRISC7 { set { SFRBitValue(148, 7 , value); } get { return SFRBitValue(148, 7); } }

        public UInt32 OSCTUNE { set { SFRValue(155, value); } get { return SFRValue(155); } }
        public UInt32 OSCTUNEbits_TUN0 { set { SFRBitValue(155, 0, value); } get { return SFRBitValue(155, 0); } }
        public UInt32 OSCTUNEbits_TUN1 { set { SFRBitValue(155, 1, value); } get { return SFRBitValue(155, 1); } }
        public UInt32 OSCTUNEbits_TUN2 { set { SFRBitValue(155, 2, value); } get { return SFRBitValue(155, 2); } }
        public UInt32 OSCTUNEbits_TUN3 { set { SFRBitValue(155, 3, value); } get { return SFRBitValue(155, 3); } }
        public UInt32 OSCTUNEbits_TUN4 { set { SFRBitValue(155, 4, value); } get { return SFRBitValue(155, 4); } }
        public UInt32 OSCTUNEbits_INTSRC { set { SFRBitValue(155, 7, value); } get { return SFRBitValue(155, 7); } }

        public UInt32 PIE1 { set { SFRValue(157, value); } get { return SFRValue(157); } }
        public UInt32 PIE1bits_TME1IE { set { SFRBitValue(157, 0, value); } get { return SFRBitValue(157, 0); } }
        public UInt32 PIE1bits_TMR2IE { set { SFRBitValue(157, 1, value); } get { return SFRBitValue(157, 1); } }
        public UInt32 PIE1bits_CCP1IE { set { SFRBitValue(157, 2, value); } get { return SFRBitValue(157, 2); } }
        public UInt32 PIE1bits_SSPIE { set { SFRBitValue(157, 3, value); } get { return SFRBitValue(157, 3); } }
        public UInt32 PIE1bits_TXIE { set { SFRBitValue(157, 4, value); } get { return SFRBitValue(157, 4); } }
        public UInt32 PIE1bits_RCIE { set { SFRBitValue(157, 5, value); } get { return SFRBitValue(157, 5); } }
        public UInt32 PIE1bits_ADIE { set { SFRBitValue(157, 6, value); } get { return SFRBitValue(157, 6); } }

        public UInt32 PIR1 { set { SFRValue(158, value); } get { return SFRValue(158); } }
        public UInt32 PIR1bits_TMR2IF { set { SFRBitValue(158, 1, value); } get { return SFRBitValue(158, 1); } }
        public UInt32 PIR1bits_TME1IF { set { SFRBitValue(158, 0, value); } get { return SFRBitValue(158, 0); } }
        public UInt32 PIR1bits_CCP1IF { set { SFRBitValue(158, 2, value); } get { return SFRBitValue(158, 2); } }
        public UInt32 PIR1bits_SSPIF { set { SFRBitValue(158, 3, value); } get { return SFRBitValue(158, 3); } }
        public UInt32 PIR1bits_TXIF { set { SFRBitValue(158, 4, value); } get { return SFRBitValue(158, 4); } }
        public UInt32 PIR1bits_RCIF { set { SFRBitValue(158, 5, value); } get { return SFRBitValue(158, 5); } }
        public UInt32 PIR1bits_ADIF { set { SFRBitValue(158, 6, value); } get { return SFRBitValue(158, 6); } }

        public UInt32 IPR1 { set { SFRValue(159, value); } get { return SFRValue(159); } }
        public UInt32 IPR1bits_TME1IP { set { SFRBitValue(159, 0, value); } get { return SFRBitValue(159, 0); } }
        public UInt32 IPR1bits_TMR2IP { set { SFRBitValue(159, 1, value); } get { return SFRBitValue(159, 1); } }
        public UInt32 IPR1bits_CCP1IP { set { SFRBitValue(159, 2, value); } get { return SFRBitValue(159, 2); } }
        public UInt32 IPR1bits_SSPIP { set { SFRBitValue(159, 3, value); } get { return SFRBitValue(159, 3); } }
        public UInt32 IPR1bits_TXIP { set { SFRBitValue(159, 4, value); } get { return SFRBitValue(159, 4); } }
        public UInt32 IPR1bits_RCIP { set { SFRBitValue(159, 5, value); } get { return SFRBitValue(159, 5); } }
        public UInt32 IPR1bits_ADIP { set { SFRBitValue(159, 6, value); } get { return SFRBitValue(159, 6); } }

        public UInt32 PIE2 { set { SFRValue(160, value); } get { return SFRValue(160); } }
        public UInt32 PIE2bits_CCP2IE { set { SFRBitValue(160, 0, value); } get { return SFRBitValue(160, 0); } }
        public UInt32 PIE2bits_TMR3IE { set { SFRBitValue(160, 1, value); } get { return SFRBitValue(160, 1); } }
        public UInt32 PIE2bits_HLVDIE { set { SFRBitValue(160, 2, value); } get { return SFRBitValue(160, 2); } }
        public UInt32 PIE2bits_BCLIE { set { SFRBitValue(160, 3, value); } get { return SFRBitValue(160, 3); } }
        public UInt32 PIE2bits_EEIE { set { SFRBitValue(160, 4, value); } get { return SFRBitValue(160, 4); } }
        public UInt32 PIE2bits_USBIE { set { SFRBitValue(160, 5, value); } get { return SFRBitValue(160, 5); } }
        public UInt32 PIE2bits_CMIE { set { SFRBitValue(160, 6, value); } get { return SFRBitValue(160, 6); } }
        public UInt32 PIE2bits_OSCFIE { set { SFRBitValue(160, 7, value); } get { return SFRBitValue(160, 7); } }

        public UInt32 PIR2 { set { SFRValue(161, value); } get { return SFRValue(161); } }
        public UInt32 PIR2bits_CCP2IF { set { SFRBitValue(161, 0, value); } get { return SFRBitValue(161, 0); } }
        public UInt32 PIR2bits_TMR3IF { set { SFRBitValue(161, 1, value); } get { return SFRBitValue(161, 1); } }
        public UInt32 PIR2bits_HLVDIF { set { SFRBitValue(161, 2, value); } get { return SFRBitValue(161, 2); } }
        public UInt32 PIR2bits_BCLIF { set { SFRBitValue(161, 3, value); } get { return SFRBitValue(161, 3); } }
        public UInt32 PIR2bits_EEIF { set { SFRBitValue(161, 4, value); } get { return SFRBitValue(161, 4); } }
        public UInt32 PIR2bits_USBIF { set { SFRBitValue(161, 5, value); } get { return SFRBitValue(161, 5); } }
        public UInt32 PIR2bits_CMIF { set { SFRBitValue(161, 6, value); } get { return SFRBitValue(161, 6); } }
        public UInt32 PIR2bits_OSCFIF { set { SFRBitValue(161, 7, value); } get { return SFRBitValue(161, 7); } }

        public UInt32 IPR2 { set { SFRValue(162, value); } get { return SFRValue(162); } }
        public UInt32 IPR2bits_CCP2IP { set { SFRBitValue(162, 0, value); } get { return SFRBitValue(162, 0); } }
        public UInt32 IPR2bits_TMR3IP { set { SFRBitValue(162, 1, value); } get { return SFRBitValue(162, 1); } }
        public UInt32 IPR2bits_HLVDIP { set { SFRBitValue(162, 2, value); } get { return SFRBitValue(162, 2); } }
        public UInt32 IPR2bits_BCLIP { set { SFRBitValue(162, 3, value); } get { return SFRBitValue(162, 3); } }
        public UInt32 IPR2bits_EEIP { set { SFRBitValue(162, 4, value); } get { return SFRBitValue(162, 4); } }
        public UInt32 IPR2bits_USBIP { set { SFRBitValue(162, 5, value); } get { return SFRBitValue(162, 5); } }
        public UInt32 IPR2bits_CMIP { set { SFRBitValue(162, 6, value); } get { return SFRBitValue(162, 6); } }
        public UInt32 IPR2bits_OSCFIP { set { SFRBitValue(162, 7, value); } get { return SFRBitValue(162, 7); } }

        public UInt32 EECON1 { set { SFRValue(166, value); } get { return SFRValue(166); } }
        public UInt32 EECON1bits_RD { set { SFRBitValue(166, 0 , value); } get { return SFRBitValue(166, 0); } }
        public UInt32 EECON1bits_WR { set { SFRBitValue(166, 1 , value); } get { return SFRBitValue(166, 1); } }
        public UInt32 EECON1bits_WREN { set { SFRBitValue(166, 2 , value); } get { return SFRBitValue(166, 2); } }
        public UInt32 EECON1bits_WRERR { set { SFRBitValue(166, 3 , value); } get { return SFRBitValue(166, 3); } }
        public UInt32 EECON1bits_FREE { set { SFRBitValue(166, 4 , value); } get { return SFRBitValue(166, 4); } }
        public UInt32 EECON1bits_CFGS { set { SFRBitValue(166, 6 , value); } get { return SFRBitValue(166, 6); } }
        public UInt32 EECON1bits_EEPGD { set { SFRBitValue(166, 7 , value); } get { return SFRBitValue(166, 7); } }

        public UInt32 RCSTA { set { SFRValue(171, value); } get { return SFRValue(171); } }
        public UInt32 RCSTAbits_RX9D { set { SFRBitValue(171, 0, value); } get { return SFRBitValue(171, 0); } }
        public UInt32 RCSTAbits_OERR { set { SFRBitValue(171, 1, value); } get { return SFRBitValue(171, 1); } }
        public UInt32 RCSTAbits_FERR { set { SFRBitValue(171, 2, value); } get { return SFRBitValue(171, 2); } }
        public UInt32 RCSTAbits_ADDEN { set { SFRBitValue(171, 3, value); } get { return SFRBitValue(171, 3); } }
        public UInt32 RCSTAbits_CREN { set { SFRBitValue(171, 4, value); } get { return SFRBitValue(171, 4); } }
        public UInt32 RCSTAbits_SREN { set { SFRBitValue(171, 5, value); } get { return SFRBitValue(171, 5); } }
        public UInt32 RCSTAbits_RX9 { set { SFRBitValue(171, 6, value); } get { return SFRBitValue(171, 6); } }
        public UInt32 RCSTAbits_SPEN { set { SFRBitValue(171, 7, value); } get { return SFRBitValue(171, 7); } }
        
        public UInt32 TXSTA { set { SFRValue(172, value); } get { return SFRValue(172); } }
        public UInt32 TXSTAbits_TX9D { set { SFRBitValue(172, 0, value); } get { return SFRBitValue(172, 0); } }
        public UInt32 TXSTAbits_TRMT { set { SFRBitValue(172, 1, value); } get { return SFRBitValue(172, 1); } }
        public UInt32 TXSTAbits_BRGH { set { SFRBitValue(172, 2, value); } get { return SFRBitValue(172, 2); } }
        public UInt32 TXSTAbits_SENDB { set { SFRBitValue(172, 3, value); } get { return SFRBitValue(172, 3); } }
        public UInt32 TXSTAbits_SYNC { set { SFRBitValue(172, 4, value); } get { return SFRBitValue(172, 4); } }
        public UInt32 TXSTAbits_TXEN { set { SFRBitValue(172, 5, value); } get { return SFRBitValue(172, 5); } }
        public UInt32 TXSTAbits_TX9 { set { SFRBitValue(172, 6, value); } get { return SFRBitValue(172, 6); } }
        public UInt32 TXSTAbits_CSRC { set { SFRBitValue(172, 7, value); } get { return SFRBitValue(172, 7); } }
        
        public UInt32 T3CON { set { SFRValue(177, value); } get { return SFRValue(177); } }
        public UInt32 T3CONbits_TMR3ON { set { SFRBitValue(177, 0, value); } get { return SFRBitValue(177, 0); } }
        public UInt32 T3CONbits_TMR3CS { set { SFRBitValue(177, 1, value); } get { return SFRBitValue(177, 1); } }
        public UInt32 T3CONbits_nT3SYNC { set { SFRBitValue(177, 2, value); } get { return SFRBitValue(177, 2); } }
        public UInt32 T3CONbits_T3CCP1 { set { SFRBitValue(177, 3, value); } get { return SFRBitValue(177, 3); } }
        public UInt32 T3CONbits_T3CKPS0 { set { SFRBitValue(177, 4, value); } get { return SFRBitValue(177, 4); } }
        public UInt32 T3CONbits_T3CKPS1 { set { SFRBitValue(177, 5, value); } get { return SFRBitValue(177, 5); } }
        public UInt32 T3CONbits_T3CCP2 { set { SFRBitValue(177, 6, value); } get { return SFRBitValue(177, 6); } }
        public UInt32 T3CONbits_RD16 { set { SFRBitValue(177, 7, value); } get { return SFRBitValue(177, 7); } }
        
        public UInt32 CMCON { set { SFRValue(180, value); } get { return SFRValue(180); } }
        public UInt32 CMCONbits_CM0 { set { SFRBitValue(180, 0, value); } get { return SFRBitValue(180, 0); } }
        public UInt32 CMCONbits_CM1 { set { SFRBitValue(180, 1, value); } get { return SFRBitValue(180, 1); } }
        public UInt32 CMCONbits_CM2 { set { SFRBitValue(180, 2, value); } get { return SFRBitValue(180, 2); } }
        public UInt32 CMCONbits_CIS { set { SFRBitValue(180, 3, value); } get { return SFRBitValue(180, 3); } }
        public UInt32 CMCONbits_C1INV { set { SFRBitValue(180, 4, value); } get { return SFRBitValue(180, 4); } }
        public UInt32 CMCONbits_C2INV { set { SFRBitValue(180, 5, value); } get { return SFRBitValue(180, 5); } }
        public UInt32 CMCONbits_C1OUT { set { SFRBitValue(180, 6, value); } get { return SFRBitValue(180, 6); } }
        public UInt32 CMCONbits_C2OUT { set { SFRBitValue(180, 7, value); } get { return SFRBitValue(180, 7); } }
        
        public UInt32 CVRCON { set { SFRValue(181, value); } get { return SFRValue(181); } }
        public UInt32 CVRCONbits_CVR0 { set { SFRBitValue(181, 0, value); } get { return SFRBitValue(181, 0); } }
        public UInt32 CVRCONbits_CVR1 { set { SFRBitValue(181, 1, value); } get { return SFRBitValue(181, 1); } }
        public UInt32 CVRCONbits_CVR2 { set { SFRBitValue(181, 2, value); } get { return SFRBitValue(181, 2); } }
        public UInt32 CVRCONbits_CVR3 { set { SFRBitValue(181, 3, value); } get { return SFRBitValue(181, 3); } }
        public UInt32 CVRCONbits_CVRSS { set { SFRBitValue(181, 4, value); } get { return SFRBitValue(181, 4); } }
        public UInt32 CVRCONbits_CVRR { set { SFRBitValue(181, 5, value); } get { return SFRBitValue(181, 5); } }
        public UInt32 CVRCONbits_CVROE { set { SFRBitValue(181, 6, value); } get { return SFRBitValue(181, 6); } }
        public UInt32 CVRCONbits_CVREN { set { SFRBitValue(181, 7, value); } get { return SFRBitValue(181, 7); } }

        public UInt32 ECCP1AS { set { SFRValue(182, value); } get { return SFRValue(182); } }
        public UInt32 ECCP1ASbits_PSSAC0 { set { SFRBitValue(182, 2, value); } get { return SFRBitValue(182, 2); } }
        public UInt32 ECCP1ASbits_PSSAC1 { set { SFRBitValue(182, 3, value); } get { return SFRBitValue(182, 3); } }
        public UInt32 ECCP1ASbits_ECCPAS0 { set { SFRBitValue(182, 4, value); } get { return SFRBitValue(182, 4); } }
        public UInt32 ECCP1ASbits_ECCPAS1 { set { SFRBitValue(182, 5, value); } get { return SFRBitValue(182, 5); } }
        public UInt32 ECCP1ASbits_ECCPAS2 { set { SFRBitValue(182, 6, value); } get { return SFRBitValue(182, 6); } }
        public UInt32 ECCP1ASbits_ECCPASE { set { SFRBitValue(182, 7, value); } get { return SFRBitValue(182, 7); } }

        public UInt32 ECCP1DEL { set { SFRValue(183, value); } get { return SFRValue(183); } }
        public UInt32 ECCP1DELbits_PRSEN { set { SFRBitValue(183, 7, value); } get { return SFRBitValue(183, 7); } }
        
        public UInt32 BAUDCON { set { SFRValue(184, value); } get { return SFRValue(184); } }
        public UInt32 BAUDCONbits_ABDEN { set { SFRBitValue(184, 0 , value); } get { return SFRBitValue(184, 0); } }
        public UInt32 BAUDCONbits_WUE { set { SFRBitValue(184, 1 , value); } get { return SFRBitValue(184, 1); } }
        public UInt32 BAUDCONbits_BRG16 { set { SFRBitValue(184, 3 , value); } get { return SFRBitValue(184, 3); } }
        public UInt32 BAUDCONbits_SCKP { set { SFRBitValue(184, 4 , value); } get { return SFRBitValue(184, 4); } }
        public UInt32 BAUDCONbits_RCIDL { set { SFRBitValue(184, 6 , value); } get { return SFRBitValue(184, 6); } }
        public UInt32 BAUDCONbits_ABDOVF { set { SFRBitValue(184, 7 , value); } get { return SFRBitValue(184, 7); } }

        public UInt32 CCP2CON { set { SFRValue(186, value); } get { return SFRValue(186); } }
        public UInt32 CCP2CONbits_CCP2M0 { set { SFRBitValue(186, 0 , value); } get { return SFRBitValue(186, 0); } }
        public UInt32 CCP2CONbits_CCP2M1 { set { SFRBitValue(186, 1 , value); } get { return SFRBitValue(186, 1); } }
        public UInt32 CCP2CONbits_CCP2M2 { set { SFRBitValue(186, 2 , value); } get { return SFRBitValue(186, 2); } }
        public UInt32 CCP2CONbits_CCP2M3 { set { SFRBitValue(186, 3 , value); } get { return SFRBitValue(186, 3); } }
        public UInt32 CCP2CONbits_DC2M3 { set { SFRBitValue(186, 4 , value); } get { return SFRBitValue(186, 4); } }
        public UInt32 CCP2CONbits_DC2B1 { set { SFRBitValue(186, 5 , value); } get { return SFRBitValue(186, 5); } }

        public UInt32 CCP1CON { set { SFRValue(189, value); } get { return SFRValue(189); } }
        public UInt32 CCP1CONbits_CCP1M0 { set { SFRBitValue(189, 0, value); } get { return SFRBitValue(189, 0); } }
        public UInt32 CCP1CONbits_CCP1M1 { set { SFRBitValue(189, 1, value); } get { return SFRBitValue(189, 1); } }
        public UInt32 CCP1CONbits_CCP1M2 { set { SFRBitValue(189, 2, value); } get { return SFRBitValue(189, 2); } }
        public UInt32 CCP1CONbits_CCP1M3 { set { SFRBitValue(189, 3, value); } get { return SFRBitValue(189, 3); } }
        public UInt32 CCP1CONbits_DC1B0 { set { SFRBitValue(189, 4, value); } get { return SFRBitValue(189, 4); } }
        public UInt32 CCP1CONbits_DC1B1 { set { SFRBitValue(189, 5, value); } get { return SFRBitValue(189, 5); } }

        public UInt32 ADCON2 { set { SFRValue(192, value); } get { return SFRValue(192); } }
        public UInt32 ADCON2bits_ADCS0 { set { SFRBitValue(192, 0 , value); } get { return SFRBitValue(192, 0); } }
        public UInt32 ADCON2bits_ADCS1 { set { SFRBitValue(192, 1 , value); } get { return SFRBitValue(192, 1); } }
        public UInt32 ADCON2bits_ADCS2 { set { SFRBitValue(192, 2 , value); } get { return SFRBitValue(192, 2); } }
        public UInt32 ADCON2bits_ACQT0 { set { SFRBitValue(192, 3 , value); } get { return SFRBitValue(192, 3); } }
        public UInt32 ADCON2bits_ACQT1 { set { SFRBitValue(192, 4 , value); } get { return SFRBitValue(192, 4); } }
        public UInt32 ADCON2bits_ACQT2 { set { SFRBitValue(192, 5 , value); } get { return SFRBitValue(192, 5); } }
        public UInt32 ADCON2bits_ADFM { set { SFRBitValue(192, 7 , value); } get { return SFRBitValue(192, 7); } }

        public UInt32 ADCON1 { set { SFRValue(193, value); } get { return SFRValue(193); } }
        public UInt32 ADCON1bits_PCFG0 { set { SFRBitValue(193, 0 , value); } get { return SFRBitValue(193, 0); } }
        public UInt32 ADCON1bits_PCFG1 { set { SFRBitValue(193, 1 , value); } get { return SFRBitValue(193, 1); } }
        public UInt32 ADCON1bits_PCFG2 { set { SFRBitValue(193, 2 , value); } get { return SFRBitValue(193, 2); } }
        public UInt32 ADCON1bits_PCFG3 { set { SFRBitValue(193, 3 , value); } get { return SFRBitValue(193, 3); } }
        public UInt32 ADCON1bits_VCFG0 { set { SFRBitValue(193, 4 , value); } get { return SFRBitValue(193, 4); } }
        public UInt32 ADCON1bits_VCFG1 { set { SFRBitValue(193, 5 , value); } get { return SFRBitValue(193, 5); } }

        public UInt32 ADCON0 { set { SFRValue(194, value); } get { return SFRValue(194); } }
        public UInt32 ADCON0bits_ADON { set { SFRBitValue(194, 0 , value); } get { return SFRBitValue(194, 0); } }
        public UInt32 ADCON0bits_GO_nDONE { set { SFRBitValue(194, 1 , value); } get { return SFRBitValue(194, 1); } }
        public UInt32 ADCON0bits_CHS0 { set { SFRBitValue(194, 2 , value); } get { return SFRBitValue(194, 2); } }
        public UInt32 ADCON0bits_CHS1 { set { SFRBitValue(194, 3 , value); } get { return SFRBitValue(194, 3); } }
        public UInt32 ADCON0bits_CHS2 { set { SFRBitValue(194, 4 , value); } get { return SFRBitValue(194, 4); } }
        public UInt32 ADCON0bits_CHS3 { set { SFRBitValue(194, 5 , value); } get { return SFRBitValue(194, 5); } }

        public UInt32 SSPCON2 { set { SFRValue(197, value); } get { return SFRValue(197); } }
        public UInt32 SSPCON2bits_SEN { set { SFRBitValue(197, 0, value); } get { return SFRBitValue(197, 0); } }
        public UInt32 SSPCON2bits_RSEN { set { SFRBitValue(197, 1, value); } get { return SFRBitValue(197, 1); } }
        public UInt32 SSPCON2bits_PEN { set { SFRBitValue(197, 2, value); } get { return SFRBitValue(197, 2); } }
        public UInt32 SSPCON2bits_RCEN { set { SFRBitValue(197, 3, value); } get { return SFRBitValue(197, 3); } }
        public UInt32 SSPCON2bits_ACKEN { set { SFRBitValue(197, 4, value); } get { return SFRBitValue(197, 4); } }
        public UInt32 SSPCON2bits_ACKDT { set { SFRBitValue(197, 5, value); } get { return SFRBitValue(197, 5); } }
        public UInt32 SSPCON2bits_ACKSTAT { set { SFRBitValue(197, 6, value); } get { return SFRBitValue(197, 6); } }
        public UInt32 SSPCON2bits_GCEN { set { SFRBitValue(197, 7, value); } get { return SFRBitValue(197, 7); } }

        public UInt32 SSPCON1 { set { SFRValue(198, value); } get { return SFRValue(198); } }
        public UInt32 SSPCON1bits_SSPM0 { set { SFRBitValue(198, 0, value); } get { return SFRBitValue(198, 0); } }
        public UInt32 SSPCON1bits_SSPM1 { set { SFRBitValue(198, 1, value); } get { return SFRBitValue(198, 1); } }
        public UInt32 SSPCON1bits_SSPM2 { set { SFRBitValue(198, 2, value); } get { return SFRBitValue(198, 2); } }
        public UInt32 SSPCON1bits_SSPM3 { set { SFRBitValue(198, 3, value); } get { return SFRBitValue(198, 3); } }
        public UInt32 SSPCON1bits_CKP { set { SFRBitValue(198, 4, value); } get { return SFRBitValue(198, 4); } }
        public UInt32 SSPCON1bits_SSPEN { set { SFRBitValue(198, 5, value); } get { return SFRBitValue(198, 5); } }
        public UInt32 SSPCON1bits_SSPOV { set { SFRBitValue(198, 6, value); } get { return SFRBitValue(198, 6); } }
        public UInt32 SSPCON1bits_WCOL { set { SFRBitValue(198, 7, value); } get { return SFRBitValue(198, 7); } }

        public UInt32 SSPSTAT { set { SFRValue(199, value); } get { return SFRValue(199); } }
        public UInt32 SSPSTATbits_BF { set { SFRBitValue(199, 0, value); } get { return SFRBitValue(199, 0); } }
        public UInt32 SSPSTATbits_UA { set { SFRBitValue(199, 1, value); } get { return SFRBitValue(199, 1); } }
        public UInt32 SSPSTATbits_R_nW { set { SFRBitValue(199, 2, value); } get { return SFRBitValue(199, 2); } }
        public UInt32 SSPSTATbits_SSPSTAT { set { SFRBitValue(199, 3, value); } get { return SFRBitValue(199, 3); } }
        public UInt32 SSPSTATbits_P { set { SFRBitValue(199, 4, value); } get { return SFRBitValue(199, 4); } }
        public UInt32 SSPSTATbits_D_nA { set { SFRBitValue(199, 5, value); } get { return SFRBitValue(199, 5); } }
        public UInt32 SSPSTATbits_CKE { set { SFRBitValue(199, 6, value); } get { return SFRBitValue(199, 6); } }
        public UInt32 SSPSTATbits_SMP { set { SFRBitValue(199, 7, value); } get { return SFRBitValue(199, 7); } }

        public UInt32 T2CON { set { SFRValue(202, value); } get { return SFRValue(202); } }
        public UInt32 T2CONbits_T2CKPS0 { set { SFRBitValue(202, 0 , value); } get { return SFRBitValue(202, 0); } }
        public UInt32 T2CONbits_T2CKPS1 { set { SFRBitValue(202, 1 , value); } get { return SFRBitValue(202, 1); } }
        public UInt32 T2CONbits_TMR2ON { set { SFRBitValue(202, 2 , value); } get { return SFRBitValue(202, 2); } }
        public UInt32 T2CONbits_T2OUTPS0 { set { SFRBitValue(202, 3 , value); } get { return SFRBitValue(202, 3); } }
        public UInt32 T2CONbits_T2OUTPS1 { set { SFRBitValue(202, 4 , value); } get { return SFRBitValue(202, 4); } }
        public UInt32 T2CONbits_T2OUTPS2 { set { SFRBitValue(202, 5 , value); } get { return SFRBitValue(202, 5); } }
        public UInt32 T2CONbits_T2OUTPS3 { set { SFRBitValue(202, 6 , value); } get { return SFRBitValue(202, 6); } }

        public UInt32 T1CON { set { SFRValue(205, value); } get { return SFRValue(205); } }
        public UInt32 T1CONbits_TMR1ON { set { SFRBitValue(205, 0, value); } get { return SFRBitValue(205, 0); } }
        public UInt32 T1CONbits_TMR1CS { set { SFRBitValue(205, 1, value); } get { return SFRBitValue(205, 1); } }
        public UInt32 T1CONbits_nT1SYNC { set { SFRBitValue(205, 2, value); } get { return SFRBitValue(205, 2); } }
        public UInt32 T1CONbits_T1OSCEN { set { SFRBitValue(205, 3, value); } get { return SFRBitValue(205, 3); } }
        public UInt32 T1CONbits_T1CKPS0 { set { SFRBitValue(205, 4, value); } get { return SFRBitValue(205, 4); } }
        public UInt32 T1CONbits_T1CKPS1 { set { SFRBitValue(205, 5, value); } get { return SFRBitValue(205, 5); } }
        public UInt32 T1CONbits_T1RUN { set { SFRBitValue(205, 6, value); } get { return SFRBitValue(205, 6); } }
        public UInt32 T1CONbits_RD16 { set { SFRBitValue(205, 7, value); } get { return SFRBitValue(205, 7); } }

        public UInt32 RCON { set { SFRValue(208, value); } get { return SFRValue(208); } }
        public UInt32 RCONbits_nBOR { set { SFRBitValue(208, 0 , value); } get { return SFRBitValue(208, 0); } }
        public UInt32 RCONbits_nPOR { set { SFRBitValue(208, 1 , value); } get { return SFRBitValue(208, 1); } }
        public UInt32 RCONbits_nPD { set { SFRBitValue(208, 2 , value); } get { return SFRBitValue(208, 2); } }
        public UInt32 RCONbits_nTO { set { SFRBitValue(208, 3 , value); } get { return SFRBitValue(208, 3); } }
        public UInt32 RCONbits_nRI { set { SFRBitValue(208, 4 , value); } get { return SFRBitValue(208, 4); } }
        public UInt32 RCONbits_SBOREN { set { SFRBitValue(208, 6 , value); } get { return SFRBitValue(208, 6); } }
        public UInt32 RCONbits_IPEN { set { SFRBitValue(208, 7 , value); } get { return SFRBitValue(208, 7); } }

        public UInt32 WDTCON { set { SFRValue(209, value); } get { return SFRValue(209); } }
        public UInt32 WDTCONbits_SWDTEN { set { SFRBitValue(209, 0, value); } get { return SFRBitValue(209, 0); } }

        public UInt32 HLVDCON { set { SFRValue(210, value); } get { return SFRValue(210); } }
        public UInt32 HLVDCONbits_HLVDL0 { set { SFRBitValue(210, 0 , value); } get { return SFRBitValue(210, 0); } }
        public UInt32 HLVDCONbits_HLVDL1 { set { SFRBitValue(210, 1 , value); } get { return SFRBitValue(210, 1); } }
        public UInt32 HLVDCONbits_HLVDL2 { set { SFRBitValue(210, 2 , value); } get { return SFRBitValue(210, 2); } }
        public UInt32 HLVDCONbits_HLVDL3 { set { SFRBitValue(210, 3 , value); } get { return SFRBitValue(210, 3); } }
        public UInt32 HLVDCONbits_HLVDEN { set { SFRBitValue(210, 4 , value); } get { return SFRBitValue(210, 4); } }
        public UInt32 HLVDCONbits_IRVST { set { SFRBitValue(210, 5 , value); } get { return SFRBitValue(210, 5); } }
        public UInt32 HLVDCONbits_VDIRMAG { set { SFRBitValue(210, 7 , value); } get { return SFRBitValue(210, 7); } }

        public UInt32 OSCCON { set { SFRValue(211, value); } get { return SFRValue(211); } }
        public UInt32 OSCCONbits_SCS0 { set { SFRBitValue(211, 0, value); } get { return SFRBitValue(211, 0); } }
        public UInt32 OSCCONbits_SCS1 { set { SFRBitValue(211, 1, value); } get { return SFRBitValue(211, 1); } }
        public UInt32 OSCCONbits_IOFS { set { SFRBitValue(211, 2, value); } get { return SFRBitValue(211, 2); } }
        public UInt32 OSCCONbits_OSTS { set { SFRBitValue(211, 3, value); } get { return SFRBitValue(211, 3); } }
        public UInt32 OSCCONbits_IRCF0 { set { SFRBitValue(211, 4, value); } get { return SFRBitValue(211, 4); } }
        public UInt32 OSCCONbits_IRCF1 { set { SFRBitValue(211, 5, value); } get { return SFRBitValue(211, 5); } }
        public UInt32 OSCCONbits_IRCF2 { set { SFRBitValue(211, 6, value); } get { return SFRBitValue(211, 6); } }
        public UInt32 OSCCONbits_IDLEN { set { SFRBitValue(211, 7, value); } get { return SFRBitValue(211, 7); } }

        public UInt32 T0CON { set { SFRValue(213, value); } get { return SFRValue(213); } }
        public UInt32 T0CONbits_T0PS0 { set { SFRBitValue(213, 0, value); } get { return SFRBitValue(213, 0); } }
        public UInt32 T0CONbits_T0PS1 { set { SFRBitValue(213, 1, value); } get { return SFRBitValue(213, 1); } }
        public UInt32 T0CONbits_T0PS2 { set { SFRBitValue(213, 2, value); } get { return SFRBitValue(213, 2); } }
        public UInt32 T0CONbits_PSA { set { SFRBitValue(213, 3, value); } get { return SFRBitValue(213, 3); } }
        public UInt32 T0CONbits_T0SE { set { SFRBitValue(213, 4, value); } get { return SFRBitValue(213, 4); } }
        public UInt32 T0CONbits_T0CS { set { SFRBitValue(213, 5, value); } get { return SFRBitValue(213, 5); } }
        public UInt32 T0CONbits_T08BIT { set { SFRBitValue(213, 6, value); } get { return SFRBitValue(213, 6); } }
        public UInt32 T0CONbits_TMR0ON { set { SFRBitValue(213, 7, value); } get { return SFRBitValue(213, 7); } }
        
        public UInt32 INTCON3 { set { SFRValue(240, value); } get { return SFRValue(240); } }
        public UInt32 INTCON3bits_INT1IF { set { SFRBitValue(240, 0 , value); } get { return SFRBitValue(240, 0); } }
        public UInt32 INTCON3bits_INT2IF { set { SFRBitValue(240, 1 , value); } get { return SFRBitValue(240, 1); } }
        public UInt32 INTCON3bits_INT1IE { set { SFRBitValue(240, 3 , value); } get { return SFRBitValue(240, 3); } }
        public UInt32 INTCON3bits_INT2IE { set { SFRBitValue(240, 4 , value); } get { return SFRBitValue(240, 4); } }
        public UInt32 INTCON3bits_INT1IP { set { SFRBitValue(240, 6 , value); } get { return SFRBitValue(240, 6); } }
        public UInt32 INTCON3bits_INT2IP { set { SFRBitValue(240, 7 , value); } get { return SFRBitValue(240, 7); } }

        public UInt32 INTCON2 { set { SFRValue(241, value); } get { return SFRValue(241); } }
        public UInt32 INTCON2bits_RBIP { set { SFRBitValue(241, 0 , value); } get { return SFRBitValue(241, 0); } }
        public UInt32 INTCON2bits_TMR0IP { set { SFRBitValue(241, 2 , value); } get { return SFRBitValue(241, 2); } }
        public UInt32 INTCON2bits_INTEDG2 { set { SFRBitValue(241, 4 , value); } get { return SFRBitValue(241, 4); } }
        public UInt32 INTCON2bits_INTEDG1 { set { SFRBitValue(241, 5 , value); } get { return SFRBitValue(241, 5); } }
        public UInt32 INTCON2bits_INTEDG0 { set { SFRBitValue(241, 6 , value); } get { return SFRBitValue(241, 6); } }
        public UInt32 INTCON2bits_nRBPU { set { SFRBitValue(241, 7 , value); } get { return SFRBitValue(241, 7); } }

        public UInt32 INTCON { set { SFRValue(242, value); } get { return SFRValue(242); } }
        public UInt32 INTCONbits_RBIF { set { SFRBitValue(242, 0, value); } get { return SFRBitValue(242, 0); } }
        public UInt32 INTCONbits_INT0IF { set { SFRBitValue(242, 1, value); } get { return SFRBitValue(242, 1); } }
        public UInt32 INTCONbits_TMR0IF { set { SFRBitValue(242, 2, value); } get { return SFRBitValue(242, 2); } }
        public UInt32 INTCONbits_RBIE { set { SFRBitValue(242, 3, value); } get { return SFRBitValue(242, 3); } }
        public UInt32 INTCONbits_INT0IE { set { SFRBitValue(242, 4, value); } get { return SFRBitValue(242, 4); } }
        public UInt32 INTCONbits_TMR0IE { set { SFRBitValue(242, 5, value); } get { return SFRBitValue(242, 5); } }
        public UInt32 INTCONbits_PEIE_GIEL { set { SFRBitValue(242, 6, value); } get { return SFRBitValue(242, 6); } }
        public UInt32 INTCONbits_GIE_GIEH { set { SFRBitValue(242, 7, value); } get { return SFRBitValue(242, 7); } }
        
        public UInt32 STKPTR { set { SFRValue(252, value); } get { return SFRValue(252); } }
        public UInt32 STKPTRbits_SP0 { set { SFRBitValue(252, 0 , value); } get { return SFRBitValue(252, 0); } }
        public UInt32 STKPTRbits_SP1 { set { SFRBitValue(252, 1 , value); } get { return SFRBitValue(252, 1); } }
        public UInt32 STKPTRbits_SP2 { set { SFRBitValue(252, 2 , value); } get { return SFRBitValue(252, 2); } }
        public UInt32 STKPTRbits_SP3 { set { SFRBitValue(252, 3 , value); } get { return SFRBitValue(252, 3); } }
        public UInt32 STKPTRbits_SP4 { set { SFRBitValue(252, 4 , value); } get { return SFRBitValue(252, 4); } }
        public UInt32 STKPTRbits_STKUNF { set { SFRBitValue(252, 6 , value); } get { return SFRBitValue(252, 6); } }
        public UInt32 STKPTRbits_STKFUL { set { SFRBitValue(252, 7 , value); } get { return SFRBitValue(252, 7); } }

        public UInt32 STATUS { get { return SFRValue(216); } }
        public UInt32 STATUSbits_C { get { return SFRBitValue(216, 0); } }
        public UInt32 STATUSbits_DC { get { return SFRBitValue(216, 1); } }
        public UInt32 STATUSbits_Z { get { return SFRBitValue(216, 2); } }
        public UInt32 STATUSbits_OV { get { return SFRBitValue(216, 3); } }
        public UInt32 STATUSbits_N { get { return SFRBitValue(216, 4); } }


        public UInt32 UFMRL { set { SFRValue(102, value); } get { return SFRValue(102); } }
        public UInt32 UFMRH { set { SFRValue(103, value); } get { return SFRValue(103); } }
        public UInt32 UIR { set { SFRValue(104, value); } get { return SFRValue(104); } }
        public UInt32 UIE { set { SFRValue(105, value); } get { return SFRValue(105); } }
        public UInt32 UEIR { set { SFRValue(106, value); } get { return SFRValue(106); } }
        public UInt32 UEIE { set { SFRValue(107, value); } get { return SFRValue(107); } }
        public UInt32 USTAT { set { SFRValue(108, value); } get { return SFRValue(108); } }
        public UInt32 UCON { set { SFRValue(109, value); } get { return SFRValue(109); } }
        public UInt32 UADDR { set { SFRValue(110, value); } get { return SFRValue(110); } }
        public UInt32 UCFG { set { SFRValue(111, value); } get { return SFRValue(111); } }
        public UInt32 UEP0 { set { SFRValue(112, value); } get { return SFRValue(112); } }
        public UInt32 UEP1 { set { SFRValue(113, value); } get { return SFRValue(113); } }
        public UInt32 UEP2 { set { SFRValue(114, value); } get { return SFRValue(114); } }
        public UInt32 UEP3 { set { SFRValue(115, value); } get { return SFRValue(115); } }
        public UInt32 UEP4 { set { SFRValue(116, value); } get { return SFRValue(116); } }
        public UInt32 UEP5 { set { SFRValue(117, value); } get { return SFRValue(117); } }
        public UInt32 UEP6 { set { SFRValue(118, value); } get { return SFRValue(118); } }
        public UInt32 UEP7 { set { SFRValue(119, value); } get { return SFRValue(119); } }
        public UInt32 UEP8 { set { SFRValue(120, value); } get { return SFRValue(120); } }
        public UInt32 UEP9 { set { SFRValue(121, value); } get { return SFRValue(121); } }
        public UInt32 UEP10 { set { SFRValue(122, value); } get { return SFRValue(122); } }
        public UInt32 UEP11 { set { SFRValue(123, value); } get { return SFRValue(123); } }
        public UInt32 UEP12 { set { SFRValue(124, value); } get { return SFRValue(124); } }
        public UInt32 UEP13 { set { SFRValue(125, value); } get { return SFRValue(125); } }
        public UInt32 UEP14 { set { SFRValue(126, value); } get { return SFRValue(126); } }
        public UInt32 UEP15 { set { SFRValue(127, value); } get { return SFRValue(127); } }
        public UInt32 EECON2 { set { SFRValue(167, value); } get { return SFRValue(167); } }
        public UInt32 EEDATA { set { SFRValue(168, value); } get { return SFRValue(168); } }
        public UInt32 EEADR { set { SFRValue(169, value); } get { return SFRValue(169); } }
        public UInt32 TXREG { set { SFRValue(173, value); } get { return SFRValue(173); } }
        public UInt32 RCREG { set { SFRValue(174, value); } get { return SFRValue(174); } }
        public UInt32 SPBRG { set { SFRValue(175, value); } get { return SFRValue(175); } }
        public UInt32 SPBRGH { set { SFRValue(176, value); } get { return SFRValue(176); } }
        public UInt32 TMR3L { set { SFRValue(178, value); } get { return SFRValue(178); } }
        public UInt32 TMR3H { set { SFRValue(179, value); } get { return SFRValue(179); } }
        public UInt32 CCPR2L { set { SFRValue(186, value); } get { return SFRValue(186); } }
        public UInt32 CCPR2H { set { SFRValue(188, value); } get { return SFRValue(188); } }
        public UInt32 CCPR1L { set { SFRValue(190, value); } get { return SFRValue(190); } }
        public UInt32 CCPR1H { set { SFRValue(191, value); } get { return SFRValue(191); } }
        public UInt32 ADRESL { set { SFRValue(195, value); } get { return SFRValue(195); } }
        public UInt32 ADRESH { set { SFRValue(196, value); } get { return SFRValue(196); } }
        public UInt32 SSPADD { set { SFRValue(200, value); } get { return SFRValue(200); } }
        public UInt32 SSPBUF { set { SFRValue(201, value); } get { return SFRValue(201); } }
        public UInt32 PR2 { set { SFRValue(203, value); } get { return SFRValue(203); } }
        public UInt32 TMR2 { set { SFRValue(204, value); } get { return SFRValue(204); } }
        public UInt32 TMR1L { set { SFRValue(206, value); } get { return SFRValue(206); } }
        public UInt32 TMR1H { set { SFRValue(207, value); } get { return SFRValue(207); } }
        public UInt32 TMR0L { set { SFRValue(214, value); } get { return SFRValue(214); } }
        public UInt32 TMR0H { set { SFRValue(215, value); } get { return SFRValue(215); } }
        public UInt32 FSR2L { set { SFRValue(217, value); } get { return SFRValue(217); } }
        public UInt32 FSR2H { set { SFRValue(218, value); } get { return SFRValue(218); } }
        public UInt32 PLUSW2 { set { SFRValue(219, value); } get { return SFRValue(219); } }
        public UInt32 PREINC2 { set { SFRValue(220, value); } get { return SFRValue(220); } }
        public UInt32 POSTDEC2 { set { SFRValue(221, value); } get { return SFRValue(221); } }
        public UInt32 POSTINC2 { set { SFRValue(222, value); } get { return SFRValue(222); } }
        public UInt32 INDF2 { set { SFRValue(223, value); } get { return SFRValue(223); } }
        public UInt32 BSR { set { SFRValue(224, value); } get { return SFRValue(224); } }
        public UInt32 FSR1L { set { SFRValue(225, value); } get { return SFRValue(225); } }
        public UInt32 FSR1H { set { SFRValue(226, value); } get { return SFRValue(226); } }
        public UInt32 PLUSW1 { set { SFRValue(227, value); } get { return SFRValue(227); } }
        public UInt32 PREINC1 { set { SFRValue(228, value); } get { return SFRValue(228); } }
        public UInt32 POSTDEC1 { set { SFRValue(229, value); } get { return SFRValue(229); } }
        public UInt32 POSTINC1 { set { SFRValue(230, value); } get { return SFRValue(230); } }
        public UInt32 INDF1 { set { SFRValue(231, value); } get { return SFRValue(231); } }
        public UInt32 WREG { set { SFRValue(232, value); } get { return SFRValue(232); } }
        public UInt32 FSR0L { set { SFRValue(233, value); } get { return SFRValue(233); } }
        public UInt32 FSR0H { set { SFRValue(234, value); } get { return SFRValue(234); } }
        public UInt32 PLUSW0 { set { SFRValue(235, value); } get { return SFRValue(235); } }
        public UInt32 PREINC0 { set { SFRValue(236, value); } get { return SFRValue(236); } }
        public UInt32 POSTDEC0 { set { SFRValue(237, value); } get { return SFRValue(237); } }
        public UInt32 POSTINC0 { set { SFRValue(238, value); } get { return SFRValue(238); } }
        public UInt32 INDF0 { set { SFRValue(239, value); } get { return SFRValue(239); } }
        public UInt32 PRODL { set { SFRValue(243, value); } get { return SFRValue(243); } }
        public UInt32 PRODH { set { SFRValue(244, value); } get { return SFRValue(244); } }
        public UInt32 TABLAT { set { SFRValue(245, value); } get { return SFRValue(245); } }
        public UInt32 TBLPTRL { set { SFRValue(246, value); } get { return SFRValue(246); } }
        public UInt32 TBLPTRH { set { SFRValue(247, value); } get { return SFRValue(247); } }
        public UInt32 TBLPTRU { set { SFRValue(248, value); } get { return SFRValue(248); } }
        public UInt32 PCL { set { SFRValue(249, value); } get { return SFRValue(249); } }
        public UInt32 PCLATH { set { SFRValue(250, value); } get { return SFRValue(250); } }
        public UInt32 PCLATU { set { SFRValue(251, value); } get { return SFRValue(251); } }
        public UInt32 TOSL { set { SFRValue(253, value); } get { return SFRValue(253); } }
        public UInt32 TOSH { set { SFRValue(254, value); } get { return SFRValue(254); } }
        public UInt32 TOSU { set { SFRValue(255, value); } get { return SFRValue(255); } }


        /*********************************************************************************************
         * The next GPIO methods are for I/O managment, experimented users of PIC devices
         * mey be more adept to use the TRISx, LATx and PORTx methods to gain access to that
         * registers of the PIC, while others may be prefer to access to PIN values, direction
         * and latch in a more general method like these GPIOx/GPIO_DIRx/GPIO_LATCHx. The use of 
         * PORTx methods or GPIOx methods (TRISx/GPIO_DIRx, LATx/GPIO_LATx) both are valid, but 
         * GPIOx methods are more slow than PORTx/TRISx/LATx methods because of how that are programed.
         *********************************************************************************************/


        //GPIO methods, is the same as use PORTx methods but a little slower
        public UInt32 GPIO0 { get { return SFRBitValue(132, 3); } }
        public UInt32 GPIO1 { set { SFRBitValue(130, 0, value); } get { return SFRBitValue(130, 0); } }
        public UInt32 GPIO2 { set { SFRBitValue(130, 1, value); } get { return SFRBitValue(130, 1); } }
        public UInt32 GPIO3 { set { SFRBitValue(130, 2, value); } get { return SFRBitValue(130, 2); } }
        public UInt32 GPIO4 { set { SFRBitValue(130, 6, value); } get { return SFRBitValue(130, 6); } }
        public UInt32 GPIO5 { set { SFRBitValue(130, 7, value); } get { return SFRBitValue(130, 7); } }
        public UInt32 GPIO6 { set { SFRBitValue(129, 0, value); } get { return SFRBitValue(129, 0); } }
        public UInt32 GPIO7 { set { SFRBitValue(129, 1, value); } get { return SFRBitValue(129, 1); } }
        public UInt32 GPIO8 { set { SFRBitValue(129, 2, value); } get { return SFRBitValue(129, 2); } }
        public UInt32 GPIO9 { set { SFRBitValue(129, 3, value); } get { return SFRBitValue(129, 3); } }
        public UInt32 GPIO10 { set { SFRBitValue(129, 4, value); } get { return SFRBitValue(129, 4); } }
        public UInt32 GPIO11 { set { SFRBitValue(129, 5, value); } get { return SFRBitValue(129, 5); } }
        public UInt32 GPIO12 { set { SFRBitValue(129, 6, value); } get { return SFRBitValue(129, 6); } }
        public UInt32 GPIO13 { set { SFRBitValue(129, 7, value); } get { return SFRBitValue(129, 7); } }
        public UInt32 GPIO14 { set { SFRBitValue(128, 0, value); } get { return SFRBitValue(128, 0); } }
        public UInt32 GPIO15 { set { SFRBitValue(128, 1, value); } get { return SFRBitValue(128, 1); } }
        public UInt32 GPIO16 { set { SFRBitValue(128, 2, value); } get { return SFRBitValue(128, 2); } }
        public UInt32 GPIO17 { set { SFRBitValue(128, 3, value); } get { return SFRBitValue(128, 3); } }
        public UInt32 GPIO18 { set { SFRBitValue(128, 4, value); } get { return SFRBitValue(128, 4); } }
        public UInt32 GPIO19 { set { SFRBitValue(128, 5, value); } get { return SFRBitValue(128, 5); } }

        //GPIO Latch methods, is the same as use LATx methods but a little slower, LATE isn't implemented on 18F2550 devices
        public UInt32 GPIO_LATCH1 { set { SFRBitValue(139, 0, value); } get { return SFRBitValue(139, 0); } }
        public UInt32 GPIO_LATCH2 { set { SFRBitValue(139, 1, value); } get { return SFRBitValue(139, 1); } }
        public UInt32 GPIO_LATCH3 { set { SFRBitValue(139, 2, value); } get { return SFRBitValue(139, 2); } }
        public UInt32 GPIO_LATCH4 { set { SFRBitValue(139, 6, value); } get { return SFRBitValue(139, 6); } }
        public UInt32 GPIO_LATCH5 { set { SFRBitValue(139, 7, value); } get { return SFRBitValue(139, 7); } }
        public UInt32 GPIO_LATCH6 { set { SFRBitValue(138, 0, value); } get { return SFRBitValue(138, 0); } }
        public UInt32 GPIO_LATCH7 { set { SFRBitValue(138, 1, value); } get { return SFRBitValue(138, 1); } }
        public UInt32 GPIO_LATCH8 { set { SFRBitValue(138, 2, value); } get { return SFRBitValue(138, 2); } }
        public UInt32 GPIO_LATCH9 { set { SFRBitValue(138, 3, value); } get { return SFRBitValue(138, 3); } }
        public UInt32 GPIO_LATCH10 { set { SFRBitValue(138, 4, value); } get { return SFRBitValue(138, 4); } }
        public UInt32 GPIO_LATCH11 { set { SFRBitValue(138, 5, value); } get { return SFRBitValue(138, 5); } }
        public UInt32 GPIO_LATCH12 { set { SFRBitValue(138, 6, value); } get { return SFRBitValue(138, 6); } }
        public UInt32 GPIO_LATCH13 { set { SFRBitValue(138, 7, value); } get { return SFRBitValue(138, 7); } }
        public UInt32 GPIO_LATCH14 { set { SFRBitValue(137, 0, value); } get { return SFRBitValue(137, 0); } }
        public UInt32 GPIO_LATCH15 { set { SFRBitValue(137, 1, value); } get { return SFRBitValue(137, 1); } }
        public UInt32 GPIO_LATCH16 { set { SFRBitValue(137, 2, value); } get { return SFRBitValue(137, 2); } }
        public UInt32 GPIO_LATCH17 { set { SFRBitValue(137, 3, value); } get { return SFRBitValue(137, 3); } }
        public UInt32 GPIO_LATCH18 { set { SFRBitValue(137, 4, value); } get { return SFRBitValue(137, 4); } }
        public UInt32 GPIO_LATCH19 { set { SFRBitValue(137, 5, value); } get { return SFRBitValue(137, 5); } }

        //GPIO Direction methods, is the same as use TRISx methods but a little slower, TRISE isn't implemented on 18F2550 devices
        public UInt32 GPIO_DIR1 { set { SFRBitValue(148, 0, value); } get { return SFRBitValue(148, 0); } }
        public UInt32 GPIO_DIR2 { set { SFRBitValue(148, 1, value); } get { return SFRBitValue(148, 1); } }
        public UInt32 GPIO_DIR3 { set { SFRBitValue(148, 2, value); } get { return SFRBitValue(148, 2); } }
        public UInt32 GPIO_DIR4 { set { SFRBitValue(148, 6, value); } get { return SFRBitValue(148, 6); } }
        public UInt32 GPIO_DIR5 { set { SFRBitValue(148, 7, value); } get { return SFRBitValue(148, 7); } }
        public UInt32 GPIO_DIR6 { set { SFRBitValue(147, 0, value); } get { return SFRBitValue(147, 0); } }
        public UInt32 GPIO_DIR7 { set { SFRBitValue(147, 1, value); } get { return SFRBitValue(147, 1); } }
        public UInt32 GPIO_DIR8 { set { SFRBitValue(147, 2, value); } get { return SFRBitValue(147, 2); } }
        public UInt32 GPIO_DIR9 { set { SFRBitValue(147, 3, value); } get { return SFRBitValue(147, 3); } }
        public UInt32 GPIO_DIR10 { set { SFRBitValue(147, 4, value); } get { return SFRBitValue(147, 4); } }
        public UInt32 GPIO_DIR11 { set { SFRBitValue(147, 5, value); } get { return SFRBitValue(147, 5); } }
        public UInt32 GPIO_DIR12 { set { SFRBitValue(147, 6, value); } get { return SFRBitValue(147, 6); } }
        public UInt32 GPIO_DIR13 { set { SFRBitValue(147, 7, value); } get { return SFRBitValue(147, 7); } }
        public UInt32 GPIO_DIR14 { set { SFRBitValue(146, 0, value); } get { return SFRBitValue(146, 0); } }
        public UInt32 GPIO_DIR15 { set { SFRBitValue(146, 1, value); } get { return SFRBitValue(146, 1); } }
        public UInt32 GPIO_DIR16 { set { SFRBitValue(146, 2, value); } get { return SFRBitValue(146, 2); } }
        public UInt32 GPIO_DIR17 { set { SFRBitValue(146, 3, value); } get { return SFRBitValue(146, 3); } }
        public UInt32 GPIO_DIR18 { set { SFRBitValue(146, 4, value); } get { return SFRBitValue(146, 4); } }
        public UInt32 GPIO_DIR19 { set { SFRBitValue(146, 5, value); } get { return SFRBitValue(146, 5); } }

        #endregion

        #region Valores para registros de configuracion de perifericos

        #region ADC

        public UInt32 ADC_CH0 { get { return 0; } }
        public UInt32 ADC_CH1 { get { return 1; } }
        public UInt32 ADC_CH2 { get { return 2; } }
        public UInt32 ADC_CH3 { get { return 3; } }
        public UInt32 ADC_CH4 { get { return 4; } }
        public UInt32 ADC_CH8 { get { return 8; } }
        public UInt32 ADC_CH9 { get { return 9; } }
        public UInt32 ADC_CH10 { get { return 10; } }
        public UInt32 ADC_CH11 { get { return 11; } }
        public UInt32 ADC_CH12 { get { return 12; } }
        public UInt32 ADC_AN0 { get { return 3584; } }
        public UInt32 ADC_AN0_To_1 { get { return 3328; } }
        public UInt32 ADC_AN0_To_2 { get { return 3072; } }
        public UInt32 ADC_AN0_To_3 { get { return 2816; } }
        public UInt32 ADC_AN0_To_4 { get { return 2560; } }
        public UInt32 ADC_AN0_To_8 { get { return 1536; } }
        public UInt32 ADC_AN0_To_9 { get { return 1280; } }
        public UInt32 ADC_AN0_To_10 { get { return 1024; } }
        public UInt32 ADC_AN0_To_11 { get { return 768; } }
        public UInt32 ADC_AN0_To_12 { get { return 0; } }
        public UInt32 ADC_TAD0 { get { return 0; } }
        public UInt32 ADC_TAD2 { get { return 524288; } }
        public UInt32 ADC_TAD4 { get { return 1048576; } }
        public UInt32 ADC_TAD6 { get { return 1572864; } }
        public UInt32 ADC_TAD8 { get { return 2097152; } }
        public UInt32 ADC_TAD12 { get { return 2949120; } }
        public UInt32 ADC_TAD16 { get { return 3145728; } }
        public UInt32 ADC_TAD20 { get { return 3670016; } }
        public UInt32 ADC_FOSC2 { get { return 0; } }
        public UInt32 ADC_FOSC8 { get { return 65536; } }
        public UInt32 ADC_FOSC32 { get { return 131072; } }
        public UInt32 ADC_ADC_FRC { get { return 196608; } }
        public UInt32 ADC_FOSC4 { get { return 262144; } }
        public UInt32 ADC_FOSC16 { get { return 327680; } }
        public UInt32 ADC_FOSC64 { get { return 393216; } }
        public UInt32 ADC_FRC_2 { get { return 458752; } }
        public UInt32 ADC_VSS_VDD { get { return 0; } }
        public UInt32 ADC_VSS_VREFp { get { return 4096; } }
        public UInt32 ADC_VREFm_VDD { get { return 8192; } }
        public UInt32 ADC_VREFm_VREFp { get { return 12228; } }
        public UInt32 ADC_LeftJust { get { return 0; } }
        public UInt32 ADC_RightJust { get { return 8388608; } }
        public UInt32 ADC_Buffer { get { return 255; } } //Buffered continous transmission

        /// <summary>
        /// All analog inputs are disabled and work as digital.
        /// </summary>
        public UInt32 CFG_ADC_AS_AllDigital { get { return 3840; } }

        /// <summary>
        /// ADC is configured with the next parameters: Tacq = 8uS, TAD = 1.333uS, AN0, CH0, Right justified, reference voltage from VDD and VSS
        /// NOTE: This configuration will set the range of channles to use only analog input 0 (ADC_AN0) automatically, anyway the user must supply a valid range in
        /// RangeOfChannels argument of ADC_CFG method
        /// </summary>
        public UInt32 CFG_ADC_AS_Generic { get { return ADC_AN0 | ADC_FOSC64 | ADC_TAD6 | ADC_RightJust | ADC_VSS_VDD; } }

        /// <summary>
        /// ADC basic configuration: Tacq = 8uS, TAD = 1.333uS, Right justified, reference voltage from VDD and VSS.
        /// NOTE: User must define the range of analog channels to use in ADC_CFG method, in any range selected the pin
        /// are configured input automatically as needed.
        /// </summary>
        public UInt32 CFG_ADC_AS_Basic { get { return ADC_FOSC64 | ADC_TAD6 | ADC_RightJust | ADC_VSS_VDD; } }

        #endregion

        #region Analog Comparator

        public UInt32 ANALOGcmp_1 { get { return 1; } }
        public UInt32 ANALOGcmp_2 { get { return 2; } }
        public UInt32 ANALOGcmp_C1INVOUT { get { return 16; } } //Comparator 1 inverted output
        public UInt32 ANALOGcmp_C1nINVOUT { get { return 0; } } //Comparator 1 not inverted output
        public UInt32 ANALOGcmp_C2INVOUT { get { return 32; } } //Comparator 2 inverted output
        public UInt32 ANALOGcmp_C2nINVOUT { get { return 0; } } //Comparator 2 not inverted output

        public UInt32 ANALOGcmp_MUX_INPUT_C1nVINRA3_C2nVINRA2 { get { return 8; } } //Only when 4Inputs multiplexed is selected
        public UInt32 ANALOGcmp_MUX_INPUT_C1nVINRA0_C2nVINRA1 { get { return 0; } } //Only when 4Inputs multiplexed is selected

        public UInt32 ANALOGcmp_CFG_COMPARATORS_RESET { get { return 0; } }
        public UInt32 ANALOGcmp_CFG_COMPARATORS_OFF { get { return 7; } }
        public UInt32 ANALOGcmp_CFG_TWO_INDEPENDENT_COMPARATORS { get { return 2; } }
        public UInt32 ANALOGcmp_CFG_TWO_INDEPENDENT_COMPARATORS_OUT_C1onRA4_C2onRA5 { get { return 3; } }
        public UInt32 ANALOGcmp_CFG_TWO_COMMON_REFERENCE_COMPARATORS_VrefOnRA3 { get { return 4; } }
        public UInt32 ANALOGcmp_CFG_TWO_COMMON_REFERENCE_COMPARATORS_VrefOnRA3_OUT_C1onRA4_C2onRA5 { get { return 5; } }
        public UInt32 ANALOGcmp_CFG_ONE_INDEPENDENT_COMPARATOR_OUT_C1onRA4 { get { return 1; } }
        public UInt32 ANALOGcmp_CFG_4INPUTS_MULTIPLEXED_TWO_COMPARATORS_VREF_FROM_MODULE { get { return 6; } }


        #endregion

        #region Master Synchronous Serial Port

        #region SPI

        public UInt32 SPI_CFG_MODE_00 { get { return 0x4000; } }
        public UInt32 SPI_CFG_MODE_01 { get { return 0; } }
        public UInt32 SPI_CFG_MODE_10 { get { return 0x4010; } }
        public UInt32 SPI_CFG_MODE_11 { get { return 0x10; } }
        public UInt32 SPI_CFG_SAMPLE_MIDDLE { get { return 0; } }
        public UInt32 SPI_CFG_SAMPLE_END { get { return 0x8000; } }
        public UInt32 SPI_CFG_SLAVE { get { return 0x04; } }
        public UInt32 SPI_CFG_MASTER { get { return 0; } }
        public UInt32 SPI_CFG_CK_FOSCdiv64 { get { return 0x02; } }
        public UInt32 SPI_CFG_CK_FOSCdiv16 { get { return 0x01; } }
        public UInt32 SPI_CFG_CK_FOSCdiv4 { get { return 0; } }
        public UInt32 SPI_CFG_CK_TMR2 { get { return 0x03; } }

        //Chip select pin for SPI module, if anyone of these would be used then the corresponding pin must be configured as output.
        public uint CS_RA0 { get { return 0; } }
        public uint CS_RA1 { get { return 1; } }
        public uint CS_RA2 { get { return 2; } }
        public uint CS_RA3 { get { return 3; } }
        public uint CS_RA4 { get { return 4; } }
        public uint CS_RA5 { get { return 5; } }
        public uint CS_RB2 { get { return 6; } }
        public uint CS_RB3 { get { return 7; } }
        public uint CS_RB4 { get { return 8; } }
        public uint CS_RB5 { get { return 9; } }
        public uint CS_RB6 { get { return 10; } }
        public uint CS_RB7 { get { return 11; } }
        public uint CS_RC0 { get { return 12; } }
        public uint CS_RC1 { get { return 13; } }
        public uint CS_RC2 { get { return 14; } }
        public uint CS_RC6 { get { return 15; } }

        #endregion

        #region I2C

        public UInt32 I2C_CFG_MASTER { get { return 0x800; } }
        public UInt32 I2C_CFG_MASTER_FRMC { get { return 0xB00; } }
        public UInt32 I2C_CFG_SlewRateEnables_400khz { get { return 0; } }
        public UInt32 I2C_CFG_SlewRateDisables_100khz_1mhz { get { return 0x80; } }
        public UInt32 I2C_CFG_SMBus_InputsEnable { get { return 0x40; } }
        public UInt32 I2C_CFG_SMBus_InputsDisable { get { return 0; } }
        public UInt32 I2C_CFG_100kHz { get { return 0x770000; } }//0x750000  clock=Fosc/(4*(SSPADD+1) | clock = 48MHz/(4*(SSPADD+1))
        public UInt32 I2C_CFG_400kHz { get { return 0x1D0000; } }//0x1B0000
        public UInt32 I2C_CFG_1MHz { get { return 0x0B0000; } }//0x090000
        public UInt32 I2C_Write { get { return 0; } }
        public UInt32 I2C_Read { get { return 1; } }
        public byte I2C_STATUS_NAK { get { return 0xFD; } }
        public byte I2C_STATUS_ACK { get { return 0xFC; } }
        public byte I2C_Repeated_Write { get { return 2; } }
        public byte I2C_Repeated_Read { get { return 1; } }
        public byte I2C_No_Repeat { get { return 0; } }


        /// <summary>
        /// Configure I2C as master at 100kHz clock
        /// </summary>
        public UInt32 CFG_I2C_AS_MASTER_100kHz { get { return I2C_CFG_MASTER | I2C_CFG_SlewRateDisables_100khz_1mhz | I2C_CFG_100kHz | I2C_CFG_SMBus_InputsEnable; } }
  
        /// <summary>
        /// Configure I2C as master at 400kHz
        /// </summary>
        public UInt32 CFG_I2C_AS_MASTER_400kHz { get { return I2C_CFG_MASTER | I2C_CFG_SlewRateEnables_400khz | I2C_CFG_400kHz | I2C_CFG_SMBus_InputsEnable; } }

        /// <summary>
        /// Configure I2C as master at 1MHz
        /// </summary>
        public UInt32 CFG_I2C_AS_MASTER_1MHz { get { return I2C_CFG_MASTER | I2C_CFG_SlewRateDisables_100khz_1mhz | I2C_CFG_1MHz | I2C_CFG_SMBus_InputsEnable; } }

        #endregion

        #region EUSART
        //TXSTA
        public UInt32 EUSART_CFG_MASTER_MODE { get{ return 0x800000; } }
        public UInt32 EUSART_CFG_SLAVE_MODE { get { return 0; } }
        public UInt32 EUSART_CFG_9BIT_TX { get { return 0x400000; } }
        public UInt32 EUSART_CFG_8BIT_TX { get { return 0; } }
        public UInt32 EUSART_CFG_TX_ENABLE { get { return 0x200000; } }
        public UInt32 EUSART_CFG_TX_DISABLE { get { return 0; } }
        public UInt32 EUSART_CFG_SYNC_MODE { get { return 0x100000; } }
        public UInt32 EUSART_CFG_ASYNC_MODE { get { return 0; } }
        public UInt32 EUSART_CFG_SEND_BRK_CHR { get { return 0x080000; } }
        public UInt32 EUSART_CFG_HIGH_SPEED { get { return 0x040000; } }
        public UInt32 EUSART_CFG_LOW_SPEED { get { return 0; } }
        //RCSTA
        public UInt32 EUSART_RX_ENABLE { get { return 0x8000; } }
        public UInt32 EUSAR_RX_DISABLE { get { return 0; } }
        public UInt32 EUSART_CFG_9BIT_RX { get { return 0x4000; } }
        public UInt32 EUSART_CFG_8BIT_RX { get { return 0; } }
        public UInt32 EUSART_SINGLE_RX { get { return 0x20; } } //EUSART_SINGLE_RX_ENABLE
        //public UInt32 EUSART_SINGLE_RX_DISABLE { get { return 0; } }
        public UInt32 EUSART_CONTINOUS_RX { get { return 0x1000; } }//EUSART_CONTINOUS_RX_ENABLE 0x0800
        //public UInt32 EUSART_CONTINOUS_RX_DISABLE { get { return 0; } }
        public UInt32 EUSART_CFG_ADDRESS_DETECT_ENABLE { get { return 0x0400; } }
        public UInt32 EUSART_CFG_ADDRESS_DETECT_DISABLE { get { return 0; } }
        //BAUDCON
        public UInt32 EUSART_CFG_SCKP_IDLE_HIGH { get { return 0x10; } }
        public UInt32 EUSART_CFG_SCKP_IDLE_LOW { get { return 0; } }
        public UInt32 EUSART_CFG_BAUD_RATE_16BIT { get { return 0x08; } }
        public UInt32 EUSART_CFG_BAUD_RATE_8BIT { get { return 0; } }
        public UInt32 EUSART_CFG_WAKEUP_ENABLE { get { return 0x02; } }
        public UInt32 EUSART_CFG_WAKEUP_DISABLE { get { return 0; } }
        public UInt32 EUSART_CFG_AUTO_BAUD_ENABLE { get { return 1; } }
        public UInt32 EUSART_CFG_AUTO_BAUD_DISABLE { get { return 0; } }


        /// <summary>
        /// Configure EUSART as Asynchronous, master mode at 8 bit Tx/Rx, and continoues reception mode
        /// </summary>
        public UInt32 CFG_EUSART_AS_Generic_Async { get { return EUSART_CFG_MASTER_MODE | EUSART_CFG_8BIT_RX | EUSART_CFG_8BIT_TX | EUSART_CFG_ASYNC_MODE | EUSART_CONTINOUS_RX; } }


        /// <summary>
        /// Configure EUSART as Synchronous, master mode, 8 bit Tx/Rx, continous reception mode, Idle state on CK as high
        /// </summary>
        public UInt32 CFG_EUSART_AS_Generic_Sync { get { return EUSART_CFG_MASTER_MODE | EUSART_CFG_8BIT_RX | EUSART_CFG_8BIT_TX | EUSART_CFG_SYNC_MODE | EUSART_CFG_SCKP_IDLE_HIGH; } }

        #endregion

        #endregion

        #region CCP

        public UInt32 CCP_CFG_Disabled { get { return 0; } }
        public UInt32 CCP_CFG_Compare_toggle { get { return 0x02; } }
        public UInt32 CCP_CFG_Compare_every_Falling { get { return 0x04; } }
        public UInt32 CCP_CFG_Capture_every_rising { get { return 0x05; } }
        public UInt32 CCP_CFG_Capture_every_4th_rising { get { return 0x06; } }
        public UInt32 CCP_CFG_Capture_every_16th_rising { get { return 0x07; } }
        public UInt32 CCP_CFG_Compare_CCPpinLowToHigh { get { return 0x08; } }
        public UInt32 CCP_CFG_Compare_CCPpinHighToLow { get { return 0x09; } }
        public UInt32 CCP_CFG_Compare_Match_Interrupt { get { return 0x0A; } }
        public UInt32 CCP_CFG_Compare_Trigger { get { return 0x0B; } }
        public byte CFG_CCP_AS_PWM_Mode { get { return 0x0F; } }
        public byte CCP1 { get { return 1; } }
        public byte CCP2 { get { return 2; } }

        private double _Fpwm;
        private double Fpwm { set { _Fpwm = value; } get { return _Fpwm; } }
        private double _PWM_TMR2Prescaler;
        private double PWM_TMR2Prescaler { set { _PWM_TMR2Prescaler = value; } get { return _PWM_TMR2Prescaler; } }

        #endregion

        #region Timer 0

        public byte TMR0_ON { get { return 0x80; } }
        public byte TMR0_8BIT { get { return 0x40; } }
        public byte TMR0_16BIT { get { return 0; } }
        public byte TMR0_clock_source_T0CKIpin { get { return 0x20; } }
        public byte TMR0_clock_source_InternalClk { get { return 0; } }
        public byte TMR0_Source_Edge_High2Low { get { return 0x10; } }
        public byte TM0_Source_Edge_Low2High { get { return 0; } }
        public byte TMR0_Prescaler_Not_Assigned { get { return 0x08; } }
        public byte TMR0_Prescaler_Assigned { get { return 0; } }
        public byte TMR0_Prescaler_1_256 { get { return 0x07; } }
        public byte TMR0_Prescaler_1_128 { get { return 0x06; } }
        public byte TMR0_Prescaler_1_64 { get { return 0x05; } }
        public byte TMR0_Prescaler_1_32 { get { return 0x04; } }
        public byte TMR0_Prescaler_1_16 { get { return 0x03; } }
        public byte TMR0_Prescaler_1_8 { get { return 0x02; } }
        public byte TMR0_Prescaler_1_4 { get { return 0x01; } }
        public byte TMR0_Prescaler_1_2 { get { return 0; } }

        #endregion

        #region Timer 1

        public byte TMR1_8BIT { get { return 0; } }
        public byte TMR1_16BIT { get { return 0x80; } }
        public byte TMR1_Device_CLK_from_TMR1 { get { return 0x40; } }
        public byte TMR1_Device_CLK_from_another_Source { get { return 0; } }
        public byte TMR1_Prescaler_1_8 { get { return 0x30; } }
        public byte TMR1_Prescaler_1_4 { get { return 0x20; } }
        public byte TMR1_Prescaler_1_2 { get { return 0x10; } }
        public byte TMR1_Prescaler_1_1 { get { return 0; } }
        public byte TMR1_Osc_Enabled { get { return 0x08; } }
        public byte TMR1_Osc_Disabled { get { return 0; } }
        public byte TMR1_Not_Sync_ext_clkin { get { return 0x04; } }
        public byte TMR1_Sync_ext_clkin { get { return 0; } }
        public byte TMR1_clk_source_RC0 { get { return 2; } }
        public byte TMR1_clk_source_InternalClk { get { return 0; } }
        public byte TMR1_On { get { return 0x01; } }
        public byte TMR1_Off { get { return 0; } }

        #endregion

        #region Timer 2

        public byte TMR2_Postscae_1_1 { get { return 0; } }
        public byte TMR2_Postscae_1_2 { get { return 0x08; } }
        public byte TMR2_Postscae_1_3 { get { return 0x10; } }
        public byte TMR2_Postscae_1_4 { get { return 0x18; } }
        public byte TMR2_Postscae_1_5 { get { return 0x20; } }
        public byte TMR2_Postscae_1_6 { get { return 0x28; } }
        public byte TMR2_Postscae_1_7 { get { return 0x30; } }
        public byte TMR2_Postscae_1_8 { get { return 0x38; } }
        public byte TMR2_Postscae_1_9 { get { return 0x40; } }
        public byte TMR2_Postscae_1_10 { get { return 0x48; } }
        public byte TMR2_Postscae_1_11 { get { return 0x50; } }
        public byte TMR2_Postscae_1_12 { get { return 0x58; } }
        public byte TMR2_Postscae_1_13 { get { return 0x60; } }
        public byte TMR2_Postscae_1_14 { get { return 0x68; } }
        public byte TMR2_Postscae_1_15 { get { return 0x70; } }
        public byte TMR2_Postscae_1_16 { get { return 0x78; } }
        public byte TMR2_Prescaler_1 { get { return 0; } }
        public byte TMR2_Prescaler_4 { get { return 1; } }
        public byte TMR2_Prescaler_16 { get { return 2; } }
            
        #endregion

        #region Timer 3

        public byte TMR3_8BIT { get { return 0; } }
        public byte TMR3_16BIT { get { return 0x80; } }
        public byte TMR3_is_CCP1_CPP2_Source { get { return 0x48; } }
        public byte TMR3_To_CCP2_TMR1_To_CCP1 { get { return 0x08; } }
        public byte TMR1_is_CCP2_CCP1_Source { get { return 0; } }
        public byte TMR3_Prescaler_1_8 { get { return 0x30; } }
        public byte TMR3_Prescaler_1_4 { get { return 0x20; } }
        public byte TMR3_Prescaler_1_2 { get { return 0x10; } }
        public byte TMR3_Prescaler_1_1 { get { return 0; } }
        public byte TMR3_Not_Sync_ext_clkin { get { return 0x04; } }
        public byte TMR3_Sync_ext_clkin { get { return 0; } }
        public byte TMR3_clk_source_RC0 { get { return 2; } }
        public byte TMR3_clk_source_InternalClk { get { return 0; } }
        public byte TMR3_On { get { return 0x01; } }
        public byte TMR3_Off { get { return 0; } }

        #endregion

        #region Comparator Voltage Reference
        public UInt32 CVREF_ON { get { return 128; } }
        public UInt32 CVREF_OFF { get { return 0; } }
        public UInt32 CVREF_CFG_OUT_RA2 { get { return 64; } }
        public UInt32 CVREF_CFG_OUT_NOT_RA2 { get { return 0; } }
        public UInt32 CVREF_CFG_LOW_RANGE { get { return 32; } }
        public UInt32 CVREF_CFG_HIGH_RANGE { get { return 0; } }
        public UInt32 CVREF_CFG_SOURCE_pVref_mVref { get { return 16; } }
        public UInt32 CVREF_CFG_SOURCE_VSS_VDD { get { return 0; } }

        double _CVrsrc;
        public double CVrsrc { set { _CVrsrc = value; } get { return _CVrsrc; } }

        /// <summary>
        /// Configure Voltage reference mdule with external ouput at pin RA2, and CVsource = VDD-VSS
        /// </summary>
        public UInt32 CFG_CVREF_AS_External { get { return CVREF_CFG_OUT_RA2 | CVREF_CFG_SOURCE_VSS_VDD; } }

        /// <summary>
        /// Configure voltage reference module with internal ouput isn't connected to RA2 but is used as Vref to analog comparator module 
        /// </summary>
        public UInt32 CFG_CVREF_AS_Internal { get { return CVREF_CFG_OUT_NOT_RA2 | CVREF_CFG_SOURCE_pVref_mVref; } }

        #endregion


        #endregion

        #region Metodos para leer y escribir datos al modulo

        /// <summary>
        /// Reads the value contained in a SFR. Some SFR are divided in two registers (low and high), 
        /// if you point to the low register this method will retrieve both low and high and it
        /// will concatenate the values.
        /// </summary>
        /// <param name="Register">SFR Address</param>
        /// <returns>The value contained in the selected SFR.</returns>
        public UInt32 SFRValue(UInt32 Register)
        {
            byte[] SndData = new byte[64];                                                              //Datos a enviar
            byte[] RcvData = new byte[64];                                                              //Datos recibidos, eco del comando enviado para verificación
                                                                            
            SndData[0] = 1;                                                                             //Cmd Leer
            SndData[1] = Convert.ToByte((Register>>8) & 0x00FF);                                        //AddrH, sin importancia para SFR ya que ese valor se completa dentro del mismo PIC
            SndData[2] = Convert.ToByte(Register & 0x00FF);                                             //AddrL

            if (WriteUSB(ref SndData) == false)                                                         //Envia el comando a la tarjeta
            {
                MessageBox.Show("RegValue error: Failed to send data.");
                return 0;
            }
            if (ReadUSB(ref RcvData, false) == true)                                                    //Espera la respuesta de la tarjeta
            {
                //Verificamos que recibio el comando correcto
                if (RcvData[0] == SndData[0])                                                           //Si los valores son iguales, el comando fue recibido correctamente
                {
                    return BitConverter.ToUInt32(RcvData, 3);// &0xFF;                                    //DAtos en bytes 4 y 5, solo 8 bits
                }
                else
                {
                    MessageBox.Show("RegValue error: verification failed.");
                    return 0;
                }
            }
            else
            {
                MessageBox.Show("RegValue error: Failed to receive data.");
                return 0;
            }
        }

        /// <summary>
        /// Writes the value in the selected SFR. Some SFR are divided in two registers (low and high),
        /// if you point to the low register this method will divide the value so it can be put in two 
        /// 8 bit registers and write to the low and high register.
        /// </summary>
        /// <param name="Register">SFR Address</param>
        /// <param name="Value">The value to be write in the SFR, 0-255 with 8bit long registers, 0-65536 with two byte long
        /// SFR but can be less in some registers like ADRESL-ADRESH that only uses 10bits of the 16 and the max value 
        /// that can be represented with 10bits is 1024</param>
        /// <returns>True if the value was written, false if not.</returns>
        public bool SFRValue(UInt32 Register, UInt32 Value)
        {
            byte[] SndData = new byte[64];                                                              //Datos a enviar
            byte[] RcvData = new byte[64];                                                              //Datos recibidos, eco del comando enviado para verificación

            //SndData[0] = 0;                                                                             //If this is not zero it wont work
            SndData[0] = 2;                                                                             //Cmd Escribir
            SndData[1] = Convert.ToByte((Register >> 8) & 0x00FF);                                      //AddrH, sin importancia para SFR ya que ese valor se completa dentro del mismo PIC
            SndData[2] = Convert.ToByte(Register & 0x00FF);                                             //AddrL
            SndData[3] = Convert.ToByte((Value & 0x00FF));                                              //DatosL
            SndData[4] = Convert.ToByte((Value >> 8) & 0x00FF);                                         //DatosH que se quiere escribir en el SFR
            

            if (WriteUSB(ref SndData) == false)                                                         //Envia el comando a la tarjeta
            {
                MessageBox.Show("RegValue error: Failed to send data.");
                return false;
            }
            if (ReadUSB(ref RcvData, false) == true)                                                    //Espera la respuesta de la tarjeta
            {
                //Verificamos que recibio el comando correcto
                if (RcvData[0] == SndData[0])                                                           //Si los valores son iguales, el comando fue recibido correctamente
                {

                    return true;
                }
                else
                {
                    MessageBox.Show("RegValue error: Verification failed");
                    return false;
                }
            }
            else
            {
                MessageBox.Show("RegValue error: Failed to receive data.");
                return false;
            }
        }

        /// <summary>
        /// Change the value of a single bit from a SFR
        /// </summary>
        /// <param name="Register">SFR Address</param>
        /// <param name="bit">Bit position 0-7</param>
        /// <param name="value">Value of the selected bit 0-1</param>
        /// <returns>True if the bit was changed, false if not.</returns>
        public bool SFRBitValue(UInt32 Register, byte bit, UInt32 value)
        {
            byte[] SndData = new byte[64];
            byte[] RcvData = new byte[64];

            //SndData[0] = 0;                                                                             //If this is not zero it wont work
            SndData[0] = SFR_CHANGE_BIT_VALUE;                                                          //CMD
            SndData[1] = Convert.ToByte((Register >> 8) & 0x00FF);                                      //AddrH, sin importancia para SFR ya que ese valor se completa dentro del mismo PIC
            SndData[2] = Convert.ToByte(Register & 0x00FF);                                             //AddrL
            SndData[3] = bit;                                                                           //Bit position to change

            if (value == 1)
            {
                SndData[4] = 1;                                                                         //Bit value
            }
            else if (value == 0)
            {
                SndData[4] = 0;                                                                         //Bit value
            }
            else
            {
                MessageBox.Show("Solo se admite como valor 1 y 0");
                return false;
            }

            if (WriteUSB(ref SndData) == false)                                                         //Envia el comando a la tarjeta
            {
                MessageBox.Show("RegBitValue error: Failed to send data.");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Read the value of a single bit from a SFR
        /// </summary>
        /// <param name="Register">SFR Address</param>
        /// <param name="bit">Bit position 0-7</param>
        /// <returns>Return 0 or 1</returns>
        public UInt32 SFRBitValue(UInt32 Register, byte bit)
        {
            byte[] SndData = new byte[64];
            byte[] RcvData = new byte[64];

            //SndData[0] = 0;                                                                             //If this is not zero it wont work
            SndData[0] = CMD_READ_BIT_VALUE;                                                            //CMD
            SndData[1] = Convert.ToByte((Register >> 8) & 0x00FF);                                      //AddrH
            SndData[2] = Convert.ToByte(Register & 0x00FF);                                             //AddrL
            SndData[3] = bit;                                                                           //Bit position to read

            if (WriteUSB(ref SndData) == false)                                                         //Envia el comando a la tarjeta
            {
                MessageBox.Show("RegBitValue error: Failed to send data.");
                return 0;
            }

            if (ReadUSB(ref RcvData, false) == true)                                                    //Espera la respuesta de la tarjeta
            {
                //Verificamos que recibio el comando correcto
                if (RcvData[0] == SndData[0])                                                           //Si los valores son iguales, el comando fue recibido correctamente
                {

                    return RcvData[1];                                                                  //DAtos en bytes 4 y 5
                }
                else
                {
                    MessageBox.Show("RegValue error: verification failed.");
                    return 0;
                }
            }
            else
            {
                MessageBox.Show("RegValue error: Failed to receive data.");
                return 0;
            }
        }

        /// <summary>
        /// Configure ADC
        /// </summary>
        /// <param name="Configuration">Configuration words for ADC, can be multiple ADC configuration words ored or use the 
        /// existent prefabricated configuration words for the ADC like CFG_ADC_AS_Generic</param>
        /// <returns></returns>
        public bool ADC_CFG(UInt32 Configuration, UInt32 RangeOfChannels)
        {
            byte[] SndData = new byte[64];
            byte[] RcvData = new byte[64];

            Configuration = Configuration | RangeOfChannels;
            //SndData[0] = 0;                                                                             //If this is not zero it wont work
            SndData[0] = CMD_ADC_CFG;                                                                   //CMD
            SndData[1] = Convert.ToByte((Configuration & 0xFF));                                        //ADCON0
            SndData[2] = Convert.ToByte((Configuration >> 8) & 0xFF);                                   //ADCON1
            SndData[3] = Convert.ToByte((Configuration >> 16) & 0xFF);                                  //ADCON2

            if (WriteUSB(ref SndData) == false)                                                         //Envia el comando a la tarjeta
            {
                MessageBox.Show("ADC Configuration error: Failed to send data.");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Turn on/off the ADC
        /// </summary>
        /// <param name="Enable">true to enable ADC, false to disable ADC</param>
        public void ADC_Enable(bool Enable)
        {
            if (Enable == true)
            {
                ADCON0bits_ADON = 1;
            }
            else if (Enable == false)
            {
                ADCON0bits_ADON = 0;
            }
        }
        
        /// <summary>
        /// Read the conversion of the selected ADC channel.
        /// The selected channel must be inside of the range of channels selected in the ADC configuration word.
        /// In 18F2550 devices Channels 5, 6, 7 don't exist.
        /// </summary>
        /// <param name="Channel">Channel of the ADC. ADC_CH0 to ADC_CH12</param>
        /// <returns>The converted value from the ADC channel selected, range 0-1024</returns>
        public UInt32 ADC_Val(UInt32 Channel)
        {
            byte[] SndData = new byte[64];                                                              //Datos a enviar
            byte[] RcvData = new byte[64];                                                              //Datos recibidos, eco del comando enviado para verificación

            //SndData[0] = 0;                                                                             //If this is not zero it wont work
            SndData[0] = CMD_ADC_Val;                                                                   //Cmd
            SndData[1] = Convert.ToByte(Channel);                                                       // Selected Channel

            if (Channel == 5 || Channel == 6 || Channel == 7)
            {
                MessageBox.Show("Channels 5, 6 and 7 can't be used in 18F2550 Devices.");
                return 0;
            }
            
            if (WriteUSB(ref SndData) == false)                                                         //Envia el comando a la tarjeta
            {
                MessageBox.Show("ADC value error: Failed to send data.");
                return 0;
            }
            if (ReadUSB(ref RcvData, false) == true)                                                    //Espera la confirmación de recepcion
            {
                //Verificamos que recibio el comando correcto y que la respuesta es la confirmación de recepcion
                if ((RcvData[0] == SndData[0]) && (RcvData[1] == 255))                                  //Si los valores son iguales, el comando fue recibido correctamente
                {
                    //Espero la respuesta al comando enviado
                    if (ReadUSB(ref RcvData, false) == true)
                    {
                        //UInt32 ADC_Conv = Convert.ToUInt32(RcvData[2]);
                        //ADC_Conv = (ADC_Conv << 8) | Convert.ToUInt32(RcvData[1]);
                        //return ADC_Conv;

                        RcvData[3] = 0;
                        RcvData[4] = 0;
                        UInt32 ADC_Conv = BitConverter.ToUInt32(RcvData, 1);
                        return ADC_Conv;
                    }
                    else
                    {
                        MessageBox.Show("ADC value error: Failed to read ADC value confirmation command.");
                        return 0;
                    }
                }
                else
                {
                    MessageBox.Show("ADC Value error: verification failed.");
                    return 0;
                }
            }
            else
            {
                MessageBox.Show("ADC Value error: Failed to receive data.");
                return 0;
            }
        }

        /// <summary>
        /// Configure the analog comparator module.
        /// </summary>
        /// <param name="Configuration">Configuration word for ANALOGcmp, can be multiple ANALOGcmp configuration words or use
        /// the prefabricated configuration words for the ANALOGcmp like ANALOGcmp_ONE_INDEPENDENT_COMPARATOR_OUT_C1onRA4</param>
        public void ANALOGcmp_CFG(UInt32 Configuration)
        {
            CMCON = Configuration;
        }

        /// <summary>
        /// Set the multiplexed inputs for both comparators, ONLY use in Four inputs multiplexed two comparators mode
        /// </summary>
        /// <param name="INPUT_SELECTION">What inputs would be used for -Vin in both comparators: RA0 and RA1 or RA3 and RA2.</param>
        public void ANLOGcmp_CFG_MUX_INPUTS(UInt32 INPUT_SELECTION)
        {
            if (INPUT_SELECTION == ANALOGcmp_MUX_INPUT_C1nVINRA0_C2nVINRA1)
            {
                CMCONbits_CIS = 0;
            }
            else if (INPUT_SELECTION == ANALOGcmp_MUX_INPUT_C1nVINRA3_C2nVINRA2)
            {
                CMCONbits_CIS = 1;
            }
            else
            {
                MessageBox.Show("Invalid input.");
                return;
            }
        }

        /// <summary>
        /// Gets the selected comparator output, a normal output of 1 indicate that Vin+ > Vin-
        /// </summary>
        /// <param name="Comparator">One of the two comparator: ANALOGcmp_1 or ANALOGcmp2 </param>
        /// <param name="Inverted">Comparator output inverted if true.</param>
        /// <returns>An integer that represents the result from the selected comparator</returns>
        public UInt32 ANALOGcmp_VALUE(UInt32 Comparator, bool Inverted)
        {

            if (Comparator == ANALOGcmp_1)
            {
                if (Inverted == true)
                {
                    CMCONbits_C1INV = 1;
                    return CMCONbits_C1OUT;
                }
                CMCONbits_C1INV = 0;
                return CMCONbits_C1OUT;
            }
            else if (Comparator == ANALOGcmp_2)
            {
                if (Inverted == true)
                {
                    CMCONbits_C2INV = 1;
                    return CMCONbits_C2OUT;
                }
                CMCONbits_C2INV = 0;
                return CMCONbits_C2OUT;
            }
            else
            {
                MessageBox.Show("Not valid Analog comparator: User provided" + Comparator.ToString());
                return 0;
            }

        }

        /// <summary>
        /// Configure the SPI module
        /// </summary>
        /// <param name="Configuration">Configuration words for SPI, can be multiple SPI configuration words ored </param>
        /// <returns>True if the SPI was configurated, false if not</returns>
        public bool SPI_CFG(UInt32 Configuration)
        {
            
            byte[] SndData = new byte[64];
            byte[] RcvData = new byte[64];

            //SndData[0] = 0;                                                                             //If this is not zero it wont work
            SndData[0] = CMD_SPI_CFG;                                                                   //CMD
            SndData[1] = Convert.ToByte((Configuration >> 8) & 0xFF);                                   //SSPSTAT
            SndData[2] = Convert.ToByte(Configuration & 0xFF);                                          //SSPCON1

            if (WriteUSB(ref SndData) == false)                                                         //Envia el comando a la tarjeta
            {
                MessageBox.Show("SPI Configuration error: Failed to send command");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Turn on/off the SPI
        /// </summary>
        /// <param name="Enable">True to on the SPI, false to off</param>
        public void SPI_Enable(bool Enable)
        {
            if (Enable == true)
            {
                SSPCON1bits_SSPEN = 1;
            }
            else if (Enable == false)
            {
                SSPCON1bits_SSPEN = 0;
            }
        }

        /// <summary>
        /// Start a transference to the SPI bus
        /// </summary>
        /// <param name="NumOfBytesRx">Number of bytes to be received 64 bytes max</param>
        /// <param name="NumOfBytesTx">Number of bytes to be send 59 bytes max</param>
        /// <param name="Exdata">Array of data to be send</param>
        /// <param name="CS_pin">Pin to be used as chip select CS_RA0 to CS_RC6, RB0, RB1 and RC7 cant't be used as chip select pin</param>
        /// <returns>Array of bytes received from the SPI bus</returns>
        public byte[] SPI_Transfer(byte NumOfBytesRx, byte NumOfBytesTx, byte[] Exdata, uint CS_pin)
        {
            byte[] SndData = new byte[64];
            byte[] RcvData = new byte[64];
            byte[] SPIRead = new byte[NumOfBytesRx];

            if (NumOfBytesRx > 64)
            {
                MessageBox.Show("Solo se pueden recibir 64 bytes de datos maximo");
                return RcvData;
            }
            else if (NumOfBytesTx > 59)
            {
                MessageBox.Show("Solo se pueden enviar 59 bytes de datos maximo");
                return RcvData;
            }

            //SndData[0] = 0;                                                                     //If this is not zero it wont work
            SndData[0] = CMD_SPI_TRANSFERENCE;                                                  //CMD
            SndData[1] = NumOfBytesRx;                                                          //Cantidad de bytes a leer
            SndData[2] = NumOfBytesTx;                                                          //Cantidad de bytes a transmitir
            SndData[3] = Convert.ToByte(CS_pin);                                                //Chip select PIN

            Exdata.CopyTo(SndData, 4);                                                          //Array de datos a intercambiar      

            if (WriteUSB(ref SndData) == false)                                                 //Envia el comando a la tarjeta
            {
                MessageBox.Show("SPI Transfer error: Failed to send data.");
                return Exdata;
            }
            if (ReadUSB(ref RcvData, false) == true)                                            //Espera la confirmación de recepcion
            {
                //Verificamos que recibio el comando correcto y que la respuesta es la confirmación de recepcion
                if ((RcvData[0] == SndData[0]) && (RcvData[1] == 255))                          //Si los valores son iguales, el comando fue recibido correctamente
                {
                    //Espero la respuesta al comando enviado
                    if (ReadUSB(ref RcvData, false) == true)
                    {
                        Array.ConstrainedCopy(RcvData, 1, SPIRead, 0, NumOfBytesRx);
                        return SPIRead;
                    }
                    else
                    {
                        MessageBox.Show("SPI Transference error: Failed confirmation command.");
                        return Exdata;
                    }
                }
                else
                {
                    MessageBox.Show("SPI Transference error: verification failed.");
                    return Exdata;
                }
            }
            else
            {
                MessageBox.Show("SPI Transference error: Failed to receive data.");
                return Exdata;
            }
        }

        /// <summary>
        /// Configure the I2C
        /// </summary>
        /// <param name="Configuration">Configuration words for I2C, can be multiple I2C configuration words ored or use the 
        /// existent prefabricated configuration words for the I2C like CFG_I2C_AS_MASTER_100kHz</param>
        /// <returns></returns>
        public bool I2C_CFG(UInt32 Configuration)
        {
            byte[] SndData = new byte[64];

            //SndData[0] = 0;                                                                     //If this is not zero it wont work
            SndData[0] = CMD_I2C_CFG;                                                           //CMD
            SndData[1] = Convert.ToByte(Configuration & 0xFF);                                  //SSPSTAT
            SndData[2] = Convert.ToByte((Configuration>>8) & 0xFF);                             //SSPCON1
            SndData[3] = Convert.ToByte((Configuration>>16) & 0xFF);                            //SSPADD BaudRate

            if (WriteUSB(ref SndData) == false)                                                 //Envia el comando a la tarjeta
            {
                MessageBox.Show("I2C Configuration error: Failed to send data.");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Turn on/off the I2C 
        /// </summary>
        /// <param name="Enable">true to on, false to off</param>
        public void I2C_Enable(bool Enable)
        {
            if (Enable == true)
            {
                SSPCON1bits_SSPEN = 1;
            }
            else if (Enable == false)
            {
                SSPCON1bits_SSPEN = 0;
            }
        }

        /// <summary>
        /// Start a I2C transference
        /// </summary>
        /// <param name="SlaveAdd">Slave Address of 7 or 10 bits</param>
        /// <param name="SlaveAddType">Slave Address type 7 or 10 bits</param>
        /// <param name="ReadWrite">set a read or write to te I2C device</param>
        /// <param name="NumOfBytesTx">Number of bytes to be send 56 bytes max</param>
        /// <param name="NumOfBytesRx">Number of bytes to be received 64 bytes max</param>
        /// <param name="TxData">Array of data to be send</param>
        /// <param name="Repeat">set a repeated start condition with a read or write</param>
        /// <returns>Array of 64 bytes long of data from the I2C device</returns>
        public byte[] I2C_Transfer(UInt32 SlaveAdd,  byte SlaveAddType, UInt32 ReadWrite, byte NumOfBytesTx, byte NumOfBytesRx, byte[] TxData, byte Repeat)
        {
            byte[] SndData = new byte[64];
            byte[] RcvData = new byte[64];
            byte[] I2CRead = new byte[NumOfBytesRx];

            if (NumOfBytesRx > 64)
            {
                MessageBox.Show("Solo se pueden recibir 64 bytes de datos maximo");
                return RcvData;
            }

            if (NumOfBytesTx > 56)
            {
                MessageBox.Show("Solo se pueden enviar 56 bytes de datos maximo");
                return RcvData;
            }


            //SndData[0] = 0;                                                                             //If this is not zero it wont work
            SndData[0] = CMD_I2C_TRANSFERENCE;                                                          //CMD
            if (SlaveAddType == 7)
            {
                SndData[1] = Convert.ToByte((SlaveAdd >> 8) & 0xFF);                                    //SLAVEADDH
                //SndData[2] = Convert.ToByte((((SlaveAdd << 1) & 0xFE) | (UInt16)ReadWrite) & 0xFF);     //SLAVEADDL with Read/Write bit
                SndData[2] = Convert.ToByte((((SlaveAdd << 1) & 0xFE) | ReadWrite) & 0xFF);
            }
            else if(SlaveAddType == 10)                                                                 //10 bit address
            {
                //SndData[1] = Convert.ToByte((((SlaveAdd >> 7) & 0xFE) | (UInt16)ReadWrite) & 0xFF);     //SLAVEADDH with R/W bit
                SndData[1] = Convert.ToByte((((SlaveAdd >> 7) & 0xFE) | ReadWrite) & 0xFF);
                SndData[2] = Convert.ToByte(SlaveAdd & 0xFF);                                           //SLAVEADDL
            }

            SndData[3] = SlaveAddType;                                                                  //7 or 10 bits
            SndData[4] = NumOfBytesTx;                                                                  //bytes to be Tx
            SndData[5] = NumOfBytesRx;                                                                  //bytes to be RX
            SndData[6] = Repeat;                                                                       //Restart
            TxData.CopyTo(SndData, 7);                                                                  //Data to be Tx

            if (WriteUSB(ref SndData) == false)                                                         //Envia el comando a la tarjeta
            {
                MessageBox.Show("I2C Transfer error: Failed to send data.");
                return TxData;
            }
            if (ReadUSB(ref RcvData, false) == true)                                                    //Espera la confirmación de recepcion
            {
                //Verificamos que recibio el comando correcto y que la respuesta es la confirmación de recepcion
                if ((RcvData[0] == SndData[0]) && (RcvData[1] == 255))                                  //Si los valores son iguales, el comando fue recibido correctamente
                {
                    //Espero la respuesta al comando enviado
                    if (ReadUSB(ref RcvData, false) == true)
                    {
                        if (RcvData[0] == I2C_STATUS_NAK)                                                      //Not Acknowledge
                        {
                            MessageBox.Show("Transmission Fail: Slave Device NAK");
                        }
                        Array.ConstrainedCopy(RcvData, 1, I2CRead, 0, NumOfBytesRx);
                        return I2CRead;
                    }
                    else
                    {
                        MessageBox.Show("I2C Transference error: Failed confirmation command.");
                        return RcvData;
                    }
                }
                else
                {
                    MessageBox.Show("I2C Transference error: verification failed.");
                    return RcvData;
                }
            }
            else
            {
                MessageBox.Show("I2C Transference error: Failed to receive data.");
                return RcvData;
            }
        }

        /// <summary>
        /// Configure the selected CCP module, only PWM mode can be used without problems, capture or compare can be 
        /// configured but is not recommended to be used because timming issues capture and compare can't work properly. 
        /// The function configure the CCPx pin as output in PWM mode.
        /// </summary>
        /// <param name="CCPModule">Selec the CCP module to config, CCP1 CPP2</param>
        /// <param name="Configuration">>Configuration words for CPP, can be multiple CPP configuration words ored or use the 
        /// existent prefabricated configuration word for the CPP tu use PWM CFG_CCP_AS_PWM_Mode</param>
        /// <returns>True if the CCP was configures with success, false if not or if a inexisten CCP module is selected</returns>
        public bool CCPx_CFG(byte CCPModule, byte Configuration)
        {
            byte[] SndData = new byte[64];

            //SndData[0] = 0;                                                                     //If this is not zero it wont work
            SndData[0] = CMD_CCP_CFG;                                                           //CMD

            if (CCPModule == 1)
            {
                SndData[1] = CCP1;                                                              //CCP module
            }
            else if (CCPModule == 2)
            {
                SndData[1] = CCP2;                                                              //CCP module
            }
            else
            {
                MessageBox.Show("No Existe el modulo CCP" + CCPModule.ToString());
                return false;
            }

            SndData[2] = Configuration;

            if (WriteUSB(ref SndData) == false)                                                 //Envia el comando a la tarjeta
            {
                MessageBox.Show("CCP Configuration error: Failed to send data.");
                return false;
            }

            return true;                                                                        //Todo bien
        }

        /// <summary>
        /// Set the Frecquency of PWM, if the value for the frequency is too high or too low this method will show and error, 
        /// and the selectes frequency will not be set.
        /// </summary>
        /// <param name="Fpwm">Frequency in Hertz</param>
        public void PWM_FPWM(double _FPWM_)
        {
            byte[] SndData = new byte[64];

            double PR2_1, PR2_2, PR2_3;
            //SndData[0] = 0;                                                                     //If this is not zero it wont work
            SndData[0] = CMD_PWM_FPWM;                                                          //CMD
            PR2_1 = 1.0 / (_FPWM_ * 4.0 * (1 / 48000000.0));                                      //TMR2 prescaler = 1
            PR2_2 = 1.0 / (_FPWM_ * 4.0 * (1 / 48000000.0) * 4.0);                                //TMR2 prescaler = 4
            PR2_3 = 1.0 / (_FPWM_ * 4.0 * (1 / 48000000.0) * 16.0);                               //TMR2 prescaler = 16

            if (PR2_1 < 255)
            {
                SndData[1] = Convert.ToByte(PR2_1);
                SndData[2] = 0;
                Fpwm = _FPWM_;
                PWM_TMR2Prescaler = 1;
            }
            else if (PR2_2 < 255)
            {
                SndData[1] = Convert.ToByte(PR2_2);
                SndData[2] = 1;
                Fpwm = _FPWM_;
                PWM_TMR2Prescaler = 4;
            }
            else if (PR2_3 < 255)
            {
                SndData[1] = Convert.ToByte(PR2_3);
                SndData[2] = 3;
                Fpwm = _FPWM_;
                PWM_TMR2Prescaler = 16;
            }
            else if (PR2_1 > 255 || PR2_2 > 255 || PR2_3 > 255)
            {
                MessageBox.Show("Fpwm value unsupported, the frequency is too low or too high.");
                return;
            }

            if (WriteUSB(ref SndData) == false)                                                 //Envia el comando a la tarjeta
            {
                MessageBox.Show("PWM FPWM error: Failed to send data.");
                return;
            }
        }

        /// <summary>
        /// Set Duty cycle of PWM
        /// </summary>
        /// <param name="CCPModule">Number of CCP module: CCP1 or CCP2</param>
        /// <param name="DT">Duty cyle in %</param>
        public void PWM_DC(UInt32 CCPModule, double DT)
        {
            byte[] SndData = new byte[64];
            double _DT; //Duty cycle in time
            UInt32 CCP_PWMbits;

            //SndData[0] = 0;                                                                     //If this is not zero it wont work
            SndData[0] = CMD_PWM_DC;                                                            //CMD

            if (CCPModule == 1)
            {
                SndData[1] = CCP1;                                                              //CCP module
            }
            else if (CCPModule == 2)
            {
                SndData[1] = CCP2;
            }

            _DT = (1 / Fpwm) * (DT / 100.0); //Given the duty cycle in percentage, calculate the Duty cycle in time
            CCP_PWMbits = Convert.ToUInt32((_DT * 48000000) / (PWM_TMR2Prescaler)); //With the duty cycle in time and the selected prescaler value calcaulate 10bits

            SndData[2] = Convert.ToByte(((CCP_PWMbits & 0x003) << 4) | 0x0F);
            SndData[3] = Convert.ToByte((CCP_PWMbits & 0xFFC) >> 2);

            if (WriteUSB(ref SndData) == false)                                                 //Envia el comando a la tarjeta
            {
                MessageBox.Show("PWM Duty Cycle error: Failed to send data.");
            }
        }

        /// <summary>
        /// Turn PWM on/off
        /// </summary>
        /// <param name="Enable">true to on the PWM, false to off</param>
        public void PWM_ENABLE(bool Enable)
        {
            if(Enable == true)
            {
                T2CONbits_TMR2ON = 1;                                                           //Timer 2 ON
            }
            else if (Enable == false)
            {
                T2CONbits_TMR2ON = 0;                                                           //Timer 2 OFF
            }
        }

        /// <summary>
        /// Configure the EUSART
        /// </summary>
        /// <param name="Configuration">Configuration words for EUSART, can be multiple EUSART configuration words ORed</param>
        public void EUSART_CFG(UInt32 Configuration)
        {
            TXSTA = (Configuration >> 16) & 0xFF;
            RCSTA = (Configuration >> 8) & 0xFF;
            BAUDCON = Configuration & 0xFF;
            TRISCbits_TRISC6 = 0;
            TRISCbits_TRISC7 = 1;
        }

        /// <summary>
        /// Set the Baudrate
        /// </summary>
        /// <param name="BR">The baudrate in bps</param>
        public void EUSART_BAUDRATE(double BR)
        {
            double[] Error = new double[3];
            double[] BRbits = new double[3];
            double[] BRcalculated = new double[3];

            BRbits[0] = Math.Ceiling((48000000.0 / (64 * BR)) - 1);
            BRbits[1] = Math.Ceiling((48000000.0 / (16 * BR)) - 1);
            BRbits[2] = Math.Ceiling((48000000.0 / (4 * BR)) - 1);
            BRcalculated[0] = 48000000.0 / (64 * (BRbits[0] + 1));
            BRcalculated[1] = 48000000.0 / (16 * (BRbits[1] + 1));
            BRcalculated[2] = 48000000.0 / (4 * (BRbits[2] + 1));

            Error[0] = Math.Abs((BRcalculated[0] - BR) / BR);
            Error[1] = Math.Abs((BRcalculated[1] - BR) / BR);
            Error[2] = Math.Abs((BRcalculated[2] - BR) / BR);

            #region Async

            if (TXSTAbits_SYNC == 0)
            {
                if (Error[0] <= Error[1])
                {
                    if (Error[0] < Error[2] && BRbits[0] <= 255)//Error 0 menor y menor a 255 bit
                    {
                        TXSTAbits_BRGH = 0;
                        BAUDCONbits_BRG16 = 0;
                        SPBRG = Convert.ToUInt32(BRbits[0]);
                        return;
                    }
                    else if (Error[0] >= Error[2])//Error 2 menor o igual a error 0
                    {
                        TXSTAbits_BRGH = 1;
                        BAUDCONbits_BRG16 = 1;
                        SPBRG = Convert.ToUInt32(BRbits[2]);
                        return;
                    }
                    Error[0] = 100; //if error 0 is lower but more than 255
                }
                else if (Error[0] >= Error[1])
                {
                    if (Error[1] < Error[2]) //Error 1 menor
                    {
                        if (BRbits[1] <= 255)
                        {
                            TXSTAbits_BRGH = 1;
                            BAUDCONbits_BRG16 = 0;
                            SPBRG = Convert.ToUInt32(BRbits[1]);
                            return;
                        }
                        else if (BRbits[1] > 255)
                        {
                            TXSTAbits_BRGH = 0;
                            BAUDCONbits_BRG16 = 1;
                            SPBRG = Convert.ToUInt32(BRbits[1]);
                            return;
                        }
                    }
                    else if (Error[1] >= Error[2]) //error 2 menor
                    {
                        TXSTAbits_BRGH = 1;
                        BAUDCONbits_BRG16 = 1;
                        SPBRG = Convert.ToUInt32(BRbits[2]);
                        return;
                    }
                }
            }

            #endregion

            #region Baudrate selection Sync

            if (TXSTAbits_SYNC == 1) //Sync mode
            {
                if (BRbits[2] > 255)
                {
                    BAUDCONbits_BRG16 = 1;
                    SPBRG = Convert.ToUInt32(BRbits[2]);
                    return;
                }
                else if (BRbits[2] <= 255)
                {
                    BAUDCONbits_BRG16 = 0;
                    SPBRG = Convert.ToUInt32(BRbits[2]);
                    return;
                }
            }

            #endregion

            if (BRbits[0] > 65535 || BRbits[1] > 65535 || BRbits[2] > 65535)
            {
                MessageBox.Show("The selected Baudrate can't be used.");
                return;
            }
        }

        /// <summary>
        /// Enable or disable the EUSART
        /// </summary>
        /// <param name="Enable">true to enable, false to disable</param>
        public void EUSART_ENABLE(bool Enable)
        {
            if (Enable == true)
            {
                RCSTAbits_SPEN = 1;
                TXSTAbits_TXEN = 1;
            }
            else if (Enable == false)
            {
                RCSTAbits_SPEN = 0;
                TXSTAbits_TXEN = 0;
            }
        }

        /// <summary>
        /// Send an array of data with the EUSART
        /// </summary>
        /// <param name="NumOfTxBytes">Number of bytes to be transmited</param>
        /// <param name="Data">Array of data to be transmited</param>
        public void EUSART_Tx(byte NumOfTxBytes, byte[] Data)
        {
            byte[] SndData = new byte[64];

            //SndData[0] = 0;                                                                     //If this is not zero it wont work
            SndData[0] = CMD_EUSART_TX;
            SndData[1] = NumOfTxBytes;
            Data.CopyTo(SndData, 2);
            if (WriteUSB(ref SndData) == false)                                                 //Envia el comando a la tarjeta
            {
                MessageBox.Show("EUSART Tx error: Failed to send data.");
            }

        }

        /// <summary>
        /// Receive data from EUSART, This is a blocking function care must be considered when Async CFG is used, threading is recommended in Async CFG.
        /// </summary>
        /// <param name="TypeRX">Tell if it's a single receive or continous receive, when Async CFG this field don't care, but must be set to any of two values.</param>
        /// <param name="NumOfRxBytes">Number of bytes to be received</param>
        /// <returns>Array of bytes with long defined by NumOfRxBytes var, max 64 bytes</returns>
        public byte[] EUSART_Rx(UInt32 TypeRX, byte NumOfRxBytes)
        {
            byte[] SndData = new byte[64];
            byte[] RcvData = new byte[64];
            byte[] EUSARTRead = new byte[NumOfRxBytes];

            //SndData[0] = 0;                                                                     //If this is not zero it wont work
            SndData[0] = CMD_EUSART_RX;
            if (TypeRX == EUSART_CONTINOUS_RX)
            {
                SndData[1] = 1;                                                                 //Single Receive
            }
            else
            {
                SndData[1] = 0;                                                                 //Continous receive
            }
            SndData[2] = NumOfRxBytes;
            if (WriteUSB(ref SndData) == false)                                                 //Envia el comando a la tarjeta
            {
                MessageBox.Show("EUSART Rx error: Failed to send data.");
                return RcvData;
            }
            if (ReadUSB(ref RcvData, false) == true)                                            
            {
                Array.ConstrainedCopy(RcvData, 0, EUSARTRead, 0, NumOfRxBytes);
                return EUSARTRead;
            }
            else
            {
                MessageBox.Show("EUSART Transference error: Failed to receive data.");
                return RcvData;
            }
        }

        /// <summary>
        /// Configure the comparator voltage reference
        /// </summary>
        ///<param name="Configuration">Configuration words for comparator voltage reference, can be multiple comparator voltage reference
        ///configuration words ored</param>
        ///<param name="CVsource">Voltaje difference between VDD-VSS (external use RA2 output) or (+Vref RA3) - (-Vref RA2) (Internal use) Max 5 Volts</param>
        public void CVREF_CFG(UInt32 Configuration, double CVsource)
        {
            if (CVsource > 5)
            {
                MessageBox.Show("CVsource can't be more than 5 volts");
                return;
            }
            CVRCON = Configuration;
            CVrsrc = CVsource;
        }

        /// <summary>
        /// Enable or disable comparator voltaje reference
        /// </summary>
        /// <param name="Enable">true enable the voltaje reference, false to disable</param>
        public void CVREF_ENABLE(bool Enable)
        {
            if (Enable == true)                                                         //Voltage reference module on
            {
                CVRCONbits_CVREN = 1;
            }
            else if (Enable == false)                                                   //Voltage reference module off
            {
                CVRCONbits_CVREN = 0;
            }
        }

        /// <summary>
        /// Set the voltage output and the PIC will try to out the more aproximate value.
        /// </summary>
        /// <param name="CVrefOut">Voltage that you want to be out from voltage reference, selec a rangre from 0 to 75% of CVsource (VDD-VSS or (Vref+) - (Vref+) it will
        /// depent from what you selected in the configuration)</param>
        public void CVREF_VALUE(double CVrefOut)
        {
            double CVRbitsLowRange = 0;
            double CVRbitsHighRange = 0;
            double d1, d2;
            UInt32 CVRCONtemp;

            if (CVrefOut > (CVrsrc * 0.75) || CVrefOut < 0)
            {
                MessageBox.Show("Voltage reference can only out values from 0 to 75% of CVsource");
                return;
            }
            else if (CVrefOut == 0)
            {
                CVRCONtemp = CVRCON & 0xF0;
                CVRCON = CVRCONtemp;
                CVRCONbits_CVRR = 1; //Low range
                return;
            }

            CVRbitsLowRange = (CVrefOut * 24) / CVrsrc;
            CVRbitsHighRange = ((CVrefOut - CVrsrc/4) * 32) / CVrsrc;

            //Check what value is more approximate to CVrefOut
            d1 = Math.Abs(CVrefOut - CVRbitsLowRange);
            d2 = Math.Abs(CVrefOut - CVRbitsHighRange);

            if (CVRbitsHighRange < 0)
            {
                CVRCONtemp = CVRCON & 0xF0;
                CVRCON = CVRCONtemp | Convert.ToByte(CVRbitsLowRange);
                CVRCONbits_CVRR = 1; //Low range
                return;
            }

            if (d1 > d2)
            {
                CVRCONtemp = CVRCON & 0xF0;
                CVRCON = CVRCONtemp | Convert.ToByte(CVRbitsHighRange);
                CVRCONbits_CVRR = 0; //High range
            }
            else if (d2 > d1)
            {
                CVRCONtemp = CVRCON & 0xF0;
                CVRCON = CVRCONtemp | Convert.ToByte(CVRbitsLowRange);
                CVRCONbits_CVRR = 1; //Low range
            }
            else if (d1 == d2)
            {
                CVRCONtemp = CVRCON & 0xF0;
                CVRCON = CVRCONtemp | Convert.ToByte(CVRbitsLowRange);
                CVRCONbits_CVRR = 1; //Low range
            }
            
        }

        /// <summary>
        /// This is only for test purpose.
        /// </summary>
        public void TEST_FCN()
        {
            byte[] SndData = new byte[64];
            //SndData[0] = 0;                                                                 //If this is not zero it wont work
            SndData[0] = CMD_TEST;
            if (WriteUSB(ref SndData) == false)                                             //Envia el comando a la tarjeta
            {
                MessageBox.Show("Test command error: Failed to send data.");
            }
            ClearRxBuffer();
            return;
        }


        /// <summary>
        /// Program a User Defined Function (UDF) into AccessB PIC flash memory.
        /// </summary>
        /// <param name="StartAddress">Start Address to be programmed.</param>
        /// <param name="ProgramData">Array with all the bytes to be programmed to AccessB PIC Flash memory.</param>
        /// <returns>True if the operation was successful, false if the operation fail.</returns>
        string[] _UDFStringData;
        public bool PROGRAM_UDF(ulong StartAddress, byte[] ProgramData)
        {
            byte[] SndData = new byte[64];
            byte[] RcvData = new byte[64];
            string[] delimiters = new string[1];
            string[] renglones, UDFStringData;
            string FileRead, ISRCallAdd, StringStartAdd;

            //Read the stream from the Hex file
            delimiters[0] = "\r\n";
            FileRead = Encoding.ASCII.GetString(ProgramData);
            renglones = FileRead.Split(delimiters, System.StringSplitOptions.None);

            uint j = 0; //Generic counter
            bool FirstTime = true;
            StringStartAdd = StartAddress.ToString();

            string Address = "4280";     //From this Address start the UDF in the 18F2550 flash program memory, this value is defined in UDF.C file, manual section

            //Search for UDF program data
            for (uint i = 0; i != renglones.Length - 5; i++)
            {
                if (renglones[i].Contains(":01" + Address) | renglones[i].Contains(":02" + Address) |
                    renglones[i].Contains(":03" + Address) | renglones[i].Contains(":04" + Address) |
                    renglones[i].Contains(":05" + Address) | renglones[i].Contains(":06" + Address) |
                    renglones[i].Contains(":07" + Address) | renglones[i].Contains(":08" + Address) |
                    renglones[i].Contains(":09" + Address) | renglones[i].Contains(":0A" + Address) |
                    renglones[i].Contains(":0B" + Address) | renglones[i].Contains(":0C" + Address) |
                    renglones[i].Contains(":0D" + Address) | renglones[i].Contains(":0E" + Address) |
                    renglones[i].Contains(":0F" + Address) | renglones[i].Contains(":10" + Address))
                {

                    //This apply only for the first encounter of UDF data in hex file.
                    if (FirstTime == true)                                              //First ocurrence of UDF data
                    {
                        FirstTime = false;
                        _UDFStringData = new string[renglones.Length - (i + 6)];        //Zero based index
                    }
                    _UDFStringData[j] = renglones[i];                                   //UDF data to be programmed
                    j++;
                }
                else if (FirstTime == false)
                {
                    if (j == _UDFStringData.Length)                                     //zero index
                    {
                        break;
                    }
                    _UDFStringData[j] = renglones[i];                                   //UDF data to be programmed
                    j++;
                }
            }

            //If there is no UDF, then stop
            if (_UDFStringData == null)
            {
                MessageBox.Show("There's no UDF program code in this hex file.");
                return false;
            }

            uint BlockNum = (uint)Math.Ceiling(_UDFStringData.Length / 2.0);
            UDFStringData = new string[BlockNum];

            //Extract only the data to be programmed in blocks of 32 bytes
            //Problema aqui, el ciclo for no llena de manera adecuada
            j = 0; //Generic purpose counter
            for (uint k = 0; k != BlockNum; k++)
            {
                try
                {
                    UDFStringData[k] = _UDFStringData[j].Substring(9, _UDFStringData[j].Length - 11) + _UDFStringData[j + 1].Substring(9, _UDFStringData[j + 1].Length - 11);
                }
                catch (Exception ex)
                {
                    UDFStringData[k] = _UDFStringData[j].Substring(9, _UDFStringData[j].Length - 11);
                }
                j = j + 2;
            }

                //If is needed fill the last byte with 0xFF value
                if (UDFStringData[UDFStringData.Length - 1].Length != 64)
                {
                    int number;
                    number = 64 - UDFStringData[UDFStringData.Length - 1].Length;
                    for (uint k = 0; k != number; k++)
                    {
                        UDFStringData[UDFStringData.Length - 1] = UDFStringData[UDFStringData.Length - 1] + "F";
                    }
                }

            //The ISR call addrress depend directly from UDF code size, we must get address to reprogram it so the ISR can be executed.
            ISRCallAdd = renglones[4].Substring(25, renglones[4].Length - 27) + renglones[5].Substring(9, renglones[5].Length - 11) + renglones[6].Substring(9, renglones[6].Length - 11) + renglones[7].Substring(9, renglones[7].Length - 11) + renglones[8].Substring(9, renglones[8].Length - 27);

            //SndData[0] = 0;
            SndData[0] = CMD_PROGRAM_UDF;
            SndData[1] = Convert.ToByte(((int)UDFStringData.Length & 0xF0) >> 8);       //BlockNum High byte
            SndData[2] = Convert.ToByte((int)UDFStringData.Length & 0x0F);              //BlockNum Low byte
            SndData[3] = Convert.ToByte((StartAddress & 0xFF00) >> 8);                  //StartAddress High byte
            SndData[4] = Convert.ToByte(StartAddress & 0xFF);                           //StartAddress Low byte

            //Send command and start address
            if (WriteUSB(ref SndData) == false)
            {
                MessageBox.Show("ProgramUDF error: Failed to send data.");
                return false;
            }

            //Wait for Successful operation
            if (ReadUSB(ref RcvData, false) == true)
            {
                if (RcvData[0] == UDF_READY)
                {
                    //The first two blocks of send pogram memory will be the ISR call opcode
                    //Send the memory blocks to be programed one by one
                    bool SendCallISROpcode = true;
                    int count = 0;
                    for (int i = 0; i != UDFStringData.Length; i++)
                    {
                        char[] TempCharData = new char[64];
                        byte[] TempUDFData = new byte[64];

                        //Convert string to char array
                        if (SendCallISROpcode == true)
                        {
                            ISRCallAdd.CopyTo(count, TempCharData, 0, 64);  //First change ISR UDF call address on program flash, the change is in one byte but 64 bytes (two memory blocks) must be rewritted due internal PIC write cycle
                            i--;                                            //Don't count this in the for loop
                            count = count + 64;
                            if (count > 64)
                            {
                                SendCallISROpcode = false;
                            }
                        }
                        else
                        {
                        UDFStringData[i].CopyTo(0, TempCharData, 0, 64);
                        }

                        //Convert each char to HEX nibbles
                        for (uint k = 0; k != 64; k++)
                        {
                            if ((int)TempCharData[k] < 0x40)
                            {
                                TempUDFData[k] = (byte)((int)TempCharData[k] - 0x30);
                            }
                            else if ((int)TempCharData[k] > 0x40)
                            {
                                TempUDFData[k] = (byte)((int)TempCharData[k] - 0x37);
                            }
                        }

                        //concatenate each pair of nibbles to form a 32 bytes block
                        j = 0;
                        //SndData[0] = 0;                                 //Always this must be zero or it will not work!!
                        for (uint k = 0; k != 64; k = k + 2)
                        {
                            SndData[j] = (byte)(((int)TempUDFData[k] << 4) | (int)(TempUDFData[k + 1]));
                            j++;
                        }

                        //Send Block
                        if (WriteUSB(ref SndData) == false)
                        {
                            MessageBox.Show("ProgramUDF error: Failed to send data.");
                            return false;
                        }

                        //Wait for Successful operation
                        if (ReadUSB(ref RcvData, false) == true)
                        {
                            if (RcvData[0] != UDF_PROGRAM_SUCCESS)
                            {
                                MessageBox.Show("ProgramUDF error at Block " + i.ToString() + ": Verify process fail");
                                return false;
                            }
                        }
                        else
                        {
                            MessageBox.Show("ProgramUDF error: Failed to receive data.");
                            return false;
                        }
                    }
                    return true;
                }
                else
                {
                    MessageBox.Show("ProgramUDF error: AccessB not ready.");
                    return false;
                }
            }
            else
            {
                MessageBox.Show("ProgramUDF error: Failed to receive data.");
                return false;
            }
        }

        /// <summary>
        /// Call the Used Defined Function (UDF) previously programmed into the AccessB PIC Flash memory.
        /// </summary>
        public void CALL_UDF()
        {
            byte[] SndData = new byte[64];

            //SndData[0] = 0;
            SndData[0] = CMD_CALL_UDF;

            if (WriteUSB(ref SndData) == false)                                             //Envia el comando a la tarjeta
            {
                MessageBox.Show("Call UDF error: Failed to send data.");
            }
        }

        /// <summary>
        /// Set the logic value of GPIO pins from GPIO1 to GPIO19, GPIO0 is input only.  
        /// </summary>
        /// <param name="value">32 bit value to be set on the 20 GPIO, GPIO0 (RE3) is input only and the value set will be ignored.</param>
        public void GPIO(UInt32 value)
        {
            UInt32 PORT = 0;
            UInt32 PORTtemp1 = 0;
            UInt32 PORTtemp2 = 0;

            //PORTA
            PORT = (value >> 13) & 0x3F;
            PORTA = PORT;

            //PORTB
            PORT = (value >> 5) & 0xFF;
            PORTB = PORT;

            //PORTC
            PORTtemp1 = value & 0x1F;
            PORTtemp2 = PORTtemp1 & 0x7;
            PORT = ((PORTtemp1 & 0xC0) << 3) | PORTtemp2;
            PORTC = PORT;
        }

        /// <summary>
        /// Get all GPIO PIN value
        /// </summary>
        /// <returns>32 bit value of all GPIO PIN, GPIO19 - GPIO0</returns>
        public UInt32 GPIO()
        {
            UInt32 PORTCtemp1, PORTCtemp2;

            PORTCtemp1 = (TRISC & 0x7);
            PORTCtemp2 = (TRISC & 0xC0) >> 3;

            return ((PORTA & 0x3F) << 14) | ((PORTB & 0xFF) << 6) | ((PORTCtemp1 | PORTCtemp2) << 1) | PORTEbits_RE3;
        }

        /// <summary>
        /// Set the value of the GPIO LATCH register, equivalent to write to the LATx registers
        /// </summary>
        /// <param name="value">32 bit value to be write on LATCH registers, GPIO0 LATCH isn't implemented on 18F2550 devies so that value is ignored.</param>
        public void GPIO_LATCH(UInt32 value)
        {
            UInt32 LATCH = 0;
            UInt32 LATCHtemp1 = 0;
            UInt32 LATCHtemp2 = 0;

            //PORTA
            LATCH = (value >> 13) & 0x3F;
            LATA = LATCH;

            //PORTB
            LATCH = (value >> 5) & 0xFF;
            LATB = LATCH;

            //PORTC
            LATCHtemp1 = value & 0x1F;
            LATCHtemp2 = LATCHtemp1 & 0x7;
            LATCH = ((LATCHtemp1 & 0xC0) << 3) | LATCHtemp2;
            LATC = LATCH;
        }

        /// <summary>
        /// Get the GPIO Latch register value for all GPIO, GPIO0 Latch register (LATE) isn't implemented on 18F2550 devices that value will be ignored.
        /// </summary>
        /// <returns>The value of all GPIO Latch registers, latch value for GPIO0 must be ignored, latch register for GPIO0 (LATE) isn't implemented on 18F2550 devices.</returns>
        public UInt32 GPIO_LATCH()
        {
            UInt32 LATCtemp1, LATCtemp2;

            LATCtemp1 = (LATC & 0x7);
            LATCtemp2 = (LATC & 0xC0) >> 3;

            return ((LATA & 0x3F) << 14) | ((LATB & 0xFF) << 6) | ((LATCtemp1 | LATCtemp2) << 1) | 0;
        }
       
        /// <summary>
        /// Set the direction of all GPIO, 1 is input 0 is output, this method is equivalent to use the TRIS registers
        /// </summary>
        /// <param name="Direction">32 bit value of the direction, GPIO0 (RE3) isn't implemented in 18F2550 devices so it's value will be ignored</param>
        public void GPIO_DIR(UInt32 Direction)
        {

            UInt32 TRIS = 0;
            UInt32 TRIStemp1 = 0;
            UInt32 TRIStemp2 = 0;

            //TRISA
            TRIS = (Direction >> 14) & 0x3F;
            TRISA = TRIS;

            //TRISB
            TRIS = (Direction >> 6) & 0xFF;
            TRISB = TRIS;

            //TRISC
            TRIStemp1 = (Direction >> 1) & 0x1F;
            TRIStemp2 = TRIStemp1 & 0x7;
            TRIS = ((TRIStemp1 <<3) & 0xC0) | TRIStemp2;
            TRISC = TRIS;
        }

        //Get GPIO direction
        /// <summary>
        /// Get the direction value of all GPIO pins, GPIO0 (RE3) isn't implemented on 18F2550 devices so it's value must be ignored.
        /// </summary>
        /// <returns>32 bit value of all GPIO direction, 1 is input, 0 is output, GPIO0 (RE3) isn't implemented on 18F2550 devices so it's value must be ignored.</returns>
        public UInt32 GPIO_DIR()
        {
            UInt32 TRISCtemp1, TRISCtemp2;

            TRISCtemp1 = TRISC & 0x7;
            TRISCtemp2 = (TRISC & 0xC0) >> 3;
            
            return ((TRISA & 0x3F) << 14) | ((TRISB & 0xFF) << 6) | (TRISCtemp1 |TRISCtemp2) << 1 | 0;
        }

        #endregion
    }
}
