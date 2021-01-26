# 03_Tubo_Read_TMX_MEM.exe

## Intro

The idea of this program is to open and read an OpenTM2 TMX file and split it.

I think the correct way to read it is defining the TMX as a C# class, and 
then serialize it as an actual class in the program. This allows complete control.

Unfortunately, TMX opentm2 provides is not a valid XML as tag ```<seg>``` includes tags 
(i.e.```<ph>```). In order to overcome this problem, a preprocess step is protect these
in CDATA section.

I.e. (Notice how the ```<ph>``` is not valid in a XML file as text.)
```
        <seg>Includes information for integrating with other
tools, such as <ph>&lt;tm tmtype=&quot;tm&quot; trademark=&quot;Microsoft&quot;&gt;</ph>Microsoft<ph>&lt;/tm&gt;</ph> Excel.</seg>
      </tuv>
```
has to be protected in order to deserialize the TMX as:

```
       <seg><![CDATA[Includes information for integrating with other
tools, such as <ph>&lt;tm tmtype=&quot;tm&quot; trademark=&quot;Microsoft&quot;&gt;</ph>Microsoft<ph>&lt;/tm&gt;</ph> Excel.]]></seg>
      </tuv>
```

The main steps are:
* You input the FILE.TMX and nnnn as the number of segments per chunk file.
* The FILE.TMX is converted to FILE_CDATA.TMX
* FILE_CDATA.TMX is read it as a class (Class definition is in TMXClas.cs)
* Class is splitted in FILE_CDATA_nnn.TMX, FILE_CDATA_2*nnnn.TMX, FILE_CDATA_3*nnnn.TMX, ...
* Each of the previous files is restored to a non CDATA version, FILE_nnnn.TMX, FILE_2*nnnn.TMX, ...

## How to run it

Solution developed in c# net.core 3.1 (so is portable win or linux) in VS2019 (but I guess would be fine with VS Code)

Syntax once compiled:
```
3_Tubo_READ_TMX_MEM path\file.TMX integer_number_to_split
```

(c) miguel canals 2021 All rights reserved