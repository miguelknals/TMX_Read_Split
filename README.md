# 03_Tubo_Read_TMX_MEM.exe

## Intro

The idea of this program is to open and read an OpenTM2 TMX file and split it.

I think the correct way to read it is defining the TMX as a C# class, and 
then serialize it as an actual class in the program. This allows complete control.

Unfortunately, TMX opentm2 provides is not a valid XML as tag <seg> includes tags 
(i.e. <ph>). In order to overcome this problem, a preprocess step is protect these
in CDATA section.

I.e.
```
        <seg>Includes information for integrating with other
tools, such as <ph>&lt;tm tmtype=&quot;tm&quot; trademark=&quot;Microsoft&quot;&gt;</ph>Microsoft<ph>&lt;/tm&gt;</ph> Excel.</seg>
      </tuv>
```
hast to be protected as:

```
       <seg><![CDATA[Includes information for integrating with other
tools, such as <ph>&lt;tm tmtype=&quot;tm&quot; trademark=&quot;Microsoft&quot;&gt;</ph>Microsoft<ph>&lt;/tm&gt;</ph> Excel.]]></seg>
      </tuv>
```