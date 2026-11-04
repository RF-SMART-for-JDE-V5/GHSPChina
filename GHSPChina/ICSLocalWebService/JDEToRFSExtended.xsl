<?xml version="1.0" encoding="UTF-8"?>

<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:import href="JDERFS_TxPSUpdateBack.xsl" />
  <xsl:import href="JDEToRFSExtendedEurope.xsl" />
  <xsl:template match="/" name="JDEToRFSCustomXSL">
    <xsl:choose>
      <xsl:when test="@name = 'RFS_TxPSUpdate'">
        <xsl:call-template name="RFS_TxPSUpdateBack">
          <xsl:with-param name="SequenceNumber" select="@app" />
        </xsl:call-template>
      </xsl:when>
      <xsl:otherwise>
        <xsl:call-template name="JDEToRFSCustomXSL_Europe" />
      </xsl:otherwise>
    </xsl:choose>
    <!-- Remove comments and add name of business function and Function back template
name-->
    <!--		<xsl:choose>
			<xsl:when test="@name = ''">
				<xsl:call-template name="">
					<xsl:with-param name="SequenceNumber" select="@app"></xsl:with-param>
				</xsl:call-template>
			</xsl:when>
			<xsl:otherwise>
-->
    <xsl:call-template name="MissingTxBack">
      <xsl:with-param name="SequenceNumber" select="@app">
      </xsl:with-param>
    </xsl:call-template>
    <!--			</xsl:otherwise>
		</xsl:choose>
-->
  </xsl:template>
</xsl:stylesheet>