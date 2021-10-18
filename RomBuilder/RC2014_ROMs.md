# Decoding RC2014 ROMs

The RC2014 uses 27C512 64k ROMs. Every ROM has an 8 digit code on it.  Each digit, 
from left to right, refers to an 8k bank from 0x0000 to 0xD000. This bank can be selected 
with the A13, A14, A15 jumpers;

| Address | A13 | A14 | A15 | ROM Label |
|---|---|---|---|---|
| 0x0000 | 0 | 0 | 0 | Xooooooo |
| 0x2000 | 0 | 0 | 1 | oXoooooo |
| 0x4000 | 0 | 1 | 0 | ooXooooo |
| 0x6000 | 0 | 1 | 1 | oooXoooo |
| 0x8000 | 1 | 0 | 0 | ooooXooo |
| 0xA000 | 1 | 0 | 1 | oooooXoo |
| 0xC000 | 1 | 1 | 0 | ooooooXo |
| 0xE000 | 1 | 1 | 1 | oooooooX |

The value of the digit represents the ROM image that sits in that particular 8k bank.  Currently, 
it will be one of the following;

- 0 – Empty bank, available for user to program
- R – Microsoft BASIC, for 32k RAM, 68B50 ACIA, with origin 0x0000
- K – Microsoft BASIC, for 56k RAM, 68B50 ACIA, with origin 0x0000
- 1 – CP/M Monitor, for pageable ROM, 64k RAM, 68B50 ACIA, CF Module at 0x10, with origin at 0x0000
- 2 – Microsoft BASIC, for 32k RAM, SIO/2, with origin 0x0000
- 4 – Microsoft BASIC, for 56k RAM, SIO/2, with origin 0x0000
- 6 – CP/M Monitor, for pageable ROM, 64k RAM, SIO/2, CF Module at 0x10, with origin at 0x0000
- 88 – Small Computer Monitor for pageable ROM, 64k RAM, SIO/2 or 68B50 ACIA, with Microsoft BASIC and CP/M boot options [Note that this is a 16k image, so Page Size needs to be set to 16k and only A14 and A15 jumpers to select]
- 9 – Small Computer Monitor for any ROM, any RAM, any UART
