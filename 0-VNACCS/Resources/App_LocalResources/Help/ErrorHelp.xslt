<?xml version="1.0" encoding="utf-8"?>

<xsl:stylesheet version="1.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

  <xsl:template match="/">
    <html>
      <xsl:apply-templates/>
    </html>
  </xsl:template>
  <xsl:template match="help">
    <body>
      <p align="left">
        <font color="#FF0000" size="5">
          <strong>
            &lt;<xsl:value-of select="@jobcode"/>&gt;
          </strong>
        </font>
      </p>
      <div align="center">
        <center/>

        <table border="2" cellpadding="4" cellspacing="3" width="100%" bordercolor="#808000" bordercolorlight="#FFFFFF">
          <tr>
            <th width="5%">
              <font color="#000000">Mã</font>
            </th>
            <th width="20%">Chỉ tiêu</th>
            <th width="10%">Mã chỉ tiêu</th>
            <th width="35%">Nội dung</th>
            <th width="30%">Giải pháp</th>
          </tr>
          <xsl:apply-templates/>
        </table>
      </div>
    </body>
    <xsl:element name="br" />
    <xsl:element name="br" />
    <xsl:element name="br" />
    <xsl:element name="br" />
    <xsl:element name="br" />
    <xsl:element name="br" />
    <xsl:element name="br" />
    <xsl:element name="br" />
    <xsl:element name="br" />
    <xsl:element name="br" />
    <xsl:element name="br" />
    <xsl:element name="br" />
    <xsl:element name="br" />
    <xsl:element name="br" />
    <xsl:element name="br" />
    <xsl:element name="br" />
    <xsl:element name="br" />
    <xsl:element name="br" />
    <xsl:element name="br" />
    <xsl:element name="br" />
    <xsl:element name="br" />
    <xsl:element name="br" />
    <xsl:element name="br" />
    <xsl:element name="br" />
    <xsl:element name="br" />
    <xsl:element name="br" />
    <xsl:element name="br" />
    <xsl:element name="br" />
    <xsl:element name="br" />
    <xsl:element name="br" />
    <xsl:element name="br" />
    <xsl:element name="br" />
    <xsl:element name="br" />
    <xsl:element name="br" />
    <xsl:element name="br" />
    <xsl:element name="br" />
    <xsl:element name="br" />
    <xsl:element name="br" />
    <xsl:element name="br" />
    <xsl:element name="br" />
    <xsl:element name="br" />
    <xsl:element name="br" />
    <xsl:element name="br" />
    <xsl:element name="br" />
    <xsl:element name="br" />
    <xsl:element name="br" />
    <xsl:element name="br" />
    <xsl:element name="br" />
    <xsl:element name="br" />
    <xsl:element name="br" />
  </xsl:template>
  <xsl:template match="response">
    <tr>

      <td align="center" valign="top" rowspan="1">
        <xsl:element name="a">
          <xsl:attribute name="name"><xsl:value-of select="@code" /></xsl:attribute>
          <xsl:value-of select="@code" />
        </xsl:element>
      </td>


      <td valign="top">
        <font color="#0000FF">
          <xsl:choose>
            <xsl:when test="contains(@name, '&#xA;')">
              <xsl:variable name="rootStr" select="@name"/>
              <xsl:call-template name="kaigyo">
                <xsl:with-param name="tmpStr">
                  <xsl:value-of select="$rootStr" />
                </xsl:with-param>
              </xsl:call-template>
            </xsl:when>
            <xsl:otherwise>
              <xsl:value-of select="@name" /><xsl:text disable-output-escaping="yes">&lt;br&gt;</xsl:text>
            </xsl:otherwise>
          </xsl:choose>
        </font>
      </td>
      <td align="center" valign="top" >
        <xsl:choose>
          <xsl:when test="contains(@id, '&#xA;')">
            <xsl:variable name="rootStr" select="@id"/>
            <xsl:call-template name="kaigyo">
              <xsl:with-param name="tmpStr">
                <xsl:value-of select="$rootStr" />
              </xsl:with-param>
            </xsl:call-template>
          </xsl:when>
          <xsl:otherwise>
            <xsl:value-of select="@id" /><xsl:text disable-output-escaping="yes">&lt;br&gt;</xsl:text>
          </xsl:otherwise>
        </xsl:choose>
      </td>
      <td valign="top">
        <xsl:choose>
          <xsl:when test="contains(description, '&#xA;')">
            <xsl:variable name="rootStr" select="description"/>
            <xsl:call-template name="kaigyo">
              <xsl:with-param name="tmpStr">
                <xsl:value-of select="$rootStr" />
              </xsl:with-param>
            </xsl:call-template>
          </xsl:when>
          <xsl:otherwise>
            <xsl:value-of select="description" /><xsl:text disable-output-escaping="yes">&lt;br&gt;</xsl:text>
          </xsl:otherwise>
        </xsl:choose>
      </td>
      <td valign="top">
        <xsl:choose>
          <xsl:when test="contains(disposition, '&#xA;')">
            <xsl:variable name="rootStr" select="disposition"/>
            <xsl:call-template name="kaigyo">
              <xsl:with-param name="tmpStr">
                <xsl:value-of select="$rootStr" />
              </xsl:with-param>
            </xsl:call-template>
          </xsl:when>
          <xsl:otherwise>
            <xsl:value-of select="disposition" /><xsl:text disable-output-escaping="yes">&lt;br&gt;</xsl:text>
          </xsl:otherwise>
        </xsl:choose>
      </td>
    </tr>
  </xsl:template>

<xsl:template name="kaigyo">
  <xsl:param name="tmpStr" />
  <xsl:choose>
    <xsl:when test="contains($tmpStr, '&#xA;')">
      <xsl:value-of select="substring-before($tmpStr, '&#xA;')" />
      <xsl:text disable-output-escaping="yes">&lt;br&gt;</xsl:text>
      <xsl:variable name="childTmpStr" select="substring-after($tmpStr, '&#xA;')"/>
      <xsl:call-template name="kaigyo">
        <xsl:with-param name="tmpStr">
          <xsl:value-of select="$childTmpStr" />
        </xsl:with-param>
      </xsl:call-template>
    </xsl:when>
    <xsl:otherwise>
      <xsl:value-of select="$tmpStr" />
    </xsl:otherwise>
  </xsl:choose>
</xsl:template>


</xsl:stylesheet>
