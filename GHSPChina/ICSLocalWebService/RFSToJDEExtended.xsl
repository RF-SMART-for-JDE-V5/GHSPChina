<?xml version="1.0" encoding="UTF-8"?>

<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:import href="TxRFS_TxPSUpdate.xsl" />
  <xsl:import href="RFSToJDEExtendedEurope.xsl" />
  <xsl:template match="/" name="RFSToJDECustomXSL">
    <xsl:choose>
      <xsl:when test="@name = 'RFS_TxPSUpdate'">
        <xsl:for-each select="Fields">
          <xsl:call-template name="RFS_TxPSUpdate" />
        </xsl:for-each>
      </xsl:when>
      <xsl:otherwise>
        <xsl:call-template name="RFSToJDECustomXSL_Europe" />
      </xsl:otherwise>
    </xsl:choose>
    <!-- Remove comments and add name of RFS Transaction and name of custom template
created -->
    <!--				<xsl:choose>
				<xsl:when test="@name = ''">
					<xsl:for-each select="Fields">
						<xsl:call-template name="" />
					</xsl:for-each>
				</xsl:when>
				</xsl:choose>
-->
  </xsl:template>
</xsl:stylesheet>