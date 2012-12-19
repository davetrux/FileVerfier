/*
Copyright (c) 2012, David Truxall
All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are met: 

1. Redistributions of source code must retain the above copyright notice, this
   list of conditions and the following disclaimer. 
2. Redistributions in binary form must reproduce the above copyright notice,
   this list of conditions and the following disclaimer in the documentation
   and/or other materials provided with the distribution. 

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR
ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
(INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

The views and conclusions contained in the software and documentation are those
of the authors and should not be interpreted as representing official policies, 
either expressed or implied, of the FreeBSD Project.
*/

using System;
using System.Linq;

namespace Dusklake.FileVerifier
{
    /// <summary>
    /// Based on data found here: http://www.garykessler.net/library/file_sigs.html
    /// </summary>
    public class Verify
    {
        public static bool IsPdf(byte[] pdfFile)
        {
            return TestSequenceHeader(Signatures.PdfHeader, pdfFile, 1024);
        }

        public static bool IsPng(byte[] pngFile)
        {
            if(TestSequenceHeader(Signatures.PngHeader, pngFile, Signatures.PngHeader.Length))
            {
                return TestSequenceTrailer(Signatures.PngTrailer, pngFile);
            }
            return false;
        }

        public static bool IsJpg(byte[] jpgFile)
        {
            if (TestSequenceHeader(Signatures.JpgHeader, jpgFile, Signatures.JpgHeader.Length))
            {
                return TestSequenceTrailer(Signatures.JpgTrailer, jpgFile);
            }
            return false;
        }

        private static bool TestSequenceTrailer(byte[] knownSequence, byte[] testFile)
        {
            var buffer = new byte[knownSequence.Length];

            var lastPositions = testFile.Length - knownSequence.Length;

            Array.Copy(testFile, lastPositions, buffer, 0, knownSequence.Length);

            if (knownSequence.SequenceEqual(buffer))
            {
                return true;
            }
            return false;
        }

        private static bool TestSequenceHeader(byte[] knownSequence, byte[] testFile, int sequenceToCheck)
        {
            var position = 0;
            var buffer = new byte[knownSequence.Length];


            while (position < sequenceToCheck)
            {
                Array.Copy(testFile, position, buffer, 0, knownSequence.Length);

                if (knownSequence.SequenceEqual(buffer))
                {
                    return true;
                }
                position++;
            }
            return false;
        }
    }
}
