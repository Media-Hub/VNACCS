<?xml version="1.0" encoding="utf-8"?>
<!-- saved from url=(0014)about:internet -->
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
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
  <!-- 1行ずつli要素変換するテンプレート -->
  <xsl:template name="autolisting">
    <xsl:param name="text" />
    <!-- 文字列が改行を含まない場合は全体をli要素で囲んで出力し、終了。 -->
    <xsl:if test="not(contains( $text, '#s' ))">
      <xsl:value-of select="$text" />
    </xsl:if>
    <!-- 文字列が改行を含む場合は、改行より前の文字列をli要素に変換し、残りは再帰させる。 -->
    <xsl:if test="contains( $text, '#s')">
      <xsl:value-of select="concat(substring-before( $text, '#s'),'[Value]')" />
      <xsl:call-template name="autolisting">
        <xsl:with-param name="text" select="substring-after( $text, '#s' )" />
      </xsl:call-template>
    </xsl:if>
  </xsl:template>
  <xsl:template match="/">
    <!--HTMLタグ変換-->
    <html>
      <!--ヘッダ部タグ変換-->
      <head>
        <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
        <title>Thông điệp của phần mềm</title>
      </head>
      <!--ボディ部タグ変換-->
      <body>
        <!--タイトル部タグ変換-->
        <h2>
          <center>Thông điệp của phần mềm</center>
        </h2>
        <xsl:apply-templates/>
      </body>
    </html>
  </xsl:template>
  <!--統合端末ソフトウェアメッセージデータ部タグ変換-->
  <xsl:template match="TermMessageSet">
    <hr>
      <!--メッセージコード-->
      <xsl:element name="a">
        <xsl:attribute name="name">
          <xsl:value-of select="@Code" />
        </xsl:attribute>
        <xsl:value-of select="@Code" />
      </xsl:element>
      <!--メッセージ-->
      <p>
        [Tin nhắn]<br/><ul>
          <b>
            <!--項目内改行の処理-->
            <xsl:choose>
              <xsl:when test="contains(Message, '&#xA;')">
                <xsl:variable name="rootStr" select="Message"/>
                <xsl:call-template name="kaigyo">
                  <xsl:with-param name="tmpStr">
                    <xsl:call-template name="autolisting">
                      <xsl:with-param name="text" select="Message"/>
                    </xsl:call-template>
                  </xsl:with-param>
                </xsl:call-template>
              </xsl:when>
              <xsl:otherwise>
                <xsl:call-template name="autolisting">
                  <xsl:with-param name="text" select="Message"/>
                </xsl:call-template>
                <xsl:text disable-output-escaping="yes">&lt;br&gt;</xsl:text>
              </xsl:otherwise>
            </xsl:choose>
          </b>
        </ul>
      </p>
      <!--説明-->
      <p>
        [Nội dung]<br/><ul>
          <!--項目内改行の処理-->
          <xsl:choose>
            <xsl:when test="contains(Description, '&#xA;')">
              <xsl:variable name="rootStr" select="Description"/>
              <xsl:call-template name="kaigyo">
                <xsl:with-param name="tmpStr">
                  <xsl:call-template name="autolisting">
                    <xsl:with-param name="text" select="Description"/>
                  </xsl:call-template>
                </xsl:with-param>
              </xsl:call-template>
            </xsl:when>
            <xsl:otherwise>
              <xsl:call-template name="autolisting">
                <xsl:with-param name="text" select="Description"/>
              </xsl:call-template>
              <xsl:text disable-output-escaping="yes">&lt;br&gt;</xsl:text>
            </xsl:otherwise>
          </xsl:choose>
        </ul>
      </p>
      <!--対処-->
      <p>
        [Giải pháp]<br/><ul>
          <!--項目内改行の処理-->
          <xsl:choose>
            <xsl:when test="contains(Disposition, '&#xA;')">
              <xsl:variable name="rootStr" select="Disposition"/>
              <xsl:call-template name="kaigyo">
                <xsl:with-param name="tmpStr">
                  <xsl:call-template name="autolisting">
                    <xsl:with-param name="text" select="Disposition"/>
                  </xsl:call-template>
                </xsl:with-param>
              </xsl:call-template>
            </xsl:when>
            <xsl:otherwise>
              <xsl:call-template name="autolisting">
                <xsl:with-param name="text" select="Disposition"/>
              </xsl:call-template>
              <xsl:text disable-output-escaping="yes">&lt;br&gt;</xsl:text>
            </xsl:otherwise>
          </xsl:choose>
        </ul>
      </p>
    </hr>
  </xsl:template>
</xsl:stylesheet>