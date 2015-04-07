using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KingWoW
{
    
    public enum KingwowKeys
    {
        None = 0,
        //
        // Riepilogo:
        //     Rappresenta il pulsante sinistro del mouse.
        LButton = 1,
        //
        // Riepilogo:
        //     Rappresenta il pulsante destro del mouse.
        RButton = 2,
        //
        // Riepilogo:
        //     Rappresenta il tasto ANNULLA.
        Cancel = 3,
        //
        // Riepilogo:
        //     Rappresenta il pulsante centrale del mouse di un mouse a tre pulsanti.
        MButton = 4,
        //
        // Riepilogo:
        //     Rappresenta il primo pulsante x del mouse in un mouse a cinque pulsanti.
        XButton1 = 5,
        //
        // Riepilogo:
        //     Rappresenta il secondo pulsante x del mouse in un mouse a cinque pulsanti.
        XButton2 = 6,
        //
        // Riepilogo:
        //     Rappresenta il tasto BACKSPACE.
        Back = 8,
        //
        // Riepilogo:
        //     The TAB key.
        Tab = 9,
        //
        // Riepilogo:
        //     The ESC key.
        Escape = 27,
        //
        // Riepilogo:
        //     Rappresenta il tasto BARRA SPAZIATRICE.
        Space = 32,
        //
        // Riepilogo:
        //     Rappresenta il tasto PGSU.
        Prior = 33,
        //
        // Riepilogo:
        //     Rappresenta il tasto PGSU.
        PageUp = 33,
        //
        // Riepilogo:
        //     The PAGE DOWN key.
        Next = 34,
        //
        // Riepilogo:
        //     The PAGE DOWN key.
        PageDown = 34,
        //
        // Riepilogo:
        //     The END key.
        End = 35,
        //
        // Riepilogo:
        //     The HOME key.
        Home = 36,
        //
        // Riepilogo:
        //     Rappresenta il tasto freccia SINISTRA.
        Left = 37,
        //
        // Riepilogo:
        //     Rappresenta il tasto freccia SU.
        Up = 38,
        //
        // Riepilogo:
        //     Rappresenta il tasto freccia DESTRA.
        Right = 39,
        //
        // Riepilogo:
        //     Rappresenta il tasto freccia GIÙ.
        Down = 40,
        //
        // Riepilogo:
        //     Rappresenta il tasto STAMPA.
        Print = 42,
        //
        // Riepilogo:
        //     Rappresenta il tasto STAMP.
        PrintScreen = 44,
        //
        // Riepilogo:
        //     Rappresenta il tasto STAMP.
        Snapshot = 44,
        //
        // Riepilogo:
        //     The INS key.
        Insert = 45,
        //
        // Riepilogo:
        //     The DEL key.
        Delete = 46,
        //
        // Riepilogo:
        //     The 0 key.
        D0 = 48,
        //
        // Riepilogo:
        //     The 1 key.
        D1 = 49,
        //
        // Riepilogo:
        //     The 2 key.
        D2 = 50,
        //
        // Riepilogo:
        //     The 3 key.
        D3 = 51,
        //
        // Riepilogo:
        //     The 4 key.
        D4 = 52,
        //
        // Riepilogo:
        //     The 5 key.
        D5 = 53,
        //
        // Riepilogo:
        //     The 6 key.
        D6 = 54,
        //
        // Riepilogo:
        //     The 7 key.
        D7 = 55,
        //
        // Riepilogo:
        //     The 8 key.
        D8 = 56,
        //
        // Riepilogo:
        //     The 9 key.
        D9 = 57,
        //
        // Riepilogo:
        //     Rappresenta il tasto A.
        A = 65,
        //
        // Riepilogo:
        //     Rappresenta il tasto B.
        B = 66,
        //
        // Riepilogo:
        //     Rappresenta il tasto C.
        C = 67,
        //
        // Riepilogo:
        //     Rappresenta il tasto D.
        D = 68,
        //
        // Riepilogo:
        //     Rappresenta il tasto E.
        E = 69,
        //
        // Riepilogo:
        //     Rappresenta il tasto F.
        F = 70,
        //
        // Riepilogo:
        //     Rappresenta il tasto G.
        G = 71,
        //
        // Riepilogo:
        //     Rappresenta il tasto H.
        H = 72,
        //
        // Riepilogo:
        //     Rappresenta il tasto I.
        I = 73,
        //
        // Riepilogo:
        //     Rappresenta il tasto J.
        J = 74,
        //
        // Riepilogo:
        //     Rappresenta il tasto K.
        K = 75,
        //
        // Riepilogo:
        //     Rappresenta il tasto L.
        L = 76,
        //
        // Riepilogo:
        //     Rappresenta il tasto M.
        M = 77,
        //
        // Riepilogo:
        //     Rappresenta il tasto N.
        N = 78,
        //
        // Riepilogo:
        //     Rappresenta il tasto O.
        O = 79,
        //
        // Riepilogo:
        //     Rappresenta il tasto P.
        P = 80,
        //
        // Riepilogo:
        //     Rappresenta il tasto Q.
        Q = 81,
        //
        // Riepilogo:
        //     Rappresenta il tasto R.
        R = 82,
        //
        // Riepilogo:
        //     Rappresenta il tasto S.
        S = 83,
        //
        // Riepilogo:
        //     Rappresenta il tasto T.
        T = 84,
        //
        // Riepilogo:
        //     Rappresenta il tasto U.
        U = 85,
        //
        // Riepilogo:
        //     Rappresenta il tasto V.
        V = 86,
        //
        // Riepilogo:
        //     Rappresenta il tasto W.
        W = 87,
        //
        // Riepilogo:
        //     Rappresenta il tasto X.
        X = 88,
        //
        // Riepilogo:
        //     Rappresenta il tasto Y.
        Y = 89,
        //
        // Riepilogo:
        //     Rappresenta il tasto Z.
        Z = 90,
        //
        // Riepilogo:
        //     The 0 key on the numeric keypad.
        NumPad0 = 96,
        //
        // Riepilogo:
        //     The 1 key on the numeric keypad.
        NumPad1 = 97,
        //
        // Riepilogo:
        //     Rappresenta il tasto 2 del tastierino numerico.
        NumPad2 = 98,
        //
        // Riepilogo:
        //     Rappresenta il tasto 3 del tastierino numerico.
        NumPad3 = 99,
        //
        // Riepilogo:
        //     Rappresenta il tasto 4 del tastierino numerico.
        NumPad4 = 100,
        //
        // Riepilogo:
        //     Rappresenta il tasto 5 del tastierino numerico.
        NumPad5 = 101,
        //
        // Riepilogo:
        //     Rappresenta il tasto 6 del tastierino numerico.
        NumPad6 = 102,
        //
        // Riepilogo:
        //     Rappresenta il tasto 7 del tastierino numerico.
        NumPad7 = 103,
        //
        // Riepilogo:
        //     The 8 key on the numeric keypad.
        NumPad8 = 104,
        //
        // Riepilogo:
        //     The 9 key on the numeric keypad.
        NumPad9 = 105,
        //
        // Riepilogo:
        //     Rappresenta il tasto di moltiplicazione.
        Multiply = 106,
        //
        // Riepilogo:
        //     Rappresenta il tasto di addizione.
        Add = 107,
        //
        // Riepilogo:
        //     Rappresenta il tasto separatore.
        Separator = 108,
        //
        // Riepilogo:
        //     Rappresenta il tasto di sottrazione.
        Subtract = 109,
        //
        // Riepilogo:
        //     The F1 key.
        F1 = 112,
        //
        // Riepilogo:
        //     The F2 key.
        F2 = 113,
        //
        // Riepilogo:
        //     The F3 key.
        F3 = 114,
        //
        // Riepilogo:
        //     The F4 key.
        F4 = 115,
        //
        // Riepilogo:
        //     The F5 key.
        F5 = 116,
        //
        // Riepilogo:
        //     The F6 key.
        F6 = 117,
        //
        // Riepilogo:
        //     The F7 key.
        F7 = 118,
        //
        // Riepilogo:
        //     The F8 key.
        F8 = 119,
        //
        // Riepilogo:
        //     The F9 key.
        F9 = 120,
        //
        // Riepilogo:
        //     The F10 key.
        F10 = 121,
        //
        // Riepilogo:
        //     The F11 key.
        F11 = 122,
        //
        // Riepilogo:
        //     The F12 key.
        F12 = 123,
 
    }
}
