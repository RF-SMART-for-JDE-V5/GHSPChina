<?xml version="1.0" encoding="UTF-8"?>

<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="xml" encoding="UTF-8" omit-xml-declaration="no" indent="yes" />
  <xsl:template match="Fields" name="RFS_TxPSUpdate">
    <callMethod app="RFSmart" name="RFS_TxPSUpdate" runOnError="no">
      <params>
        <param name="szLabelName">
          <xsl:value-of select="Field[@name = 'LabelName']/@value" />
        </param>
        <param name="cMode">
          <xsl:value-of select="Field[@name = 'Mode']/@value" />
        </param>
        <param name="cErrorCode">
          <xsl:value-of select="Field[@name = 'ErrorCode']/@value" />
        </param>
        <param name="szErrorDescr">
          <xsl:value-of select="Field[@name = 'ErrorDescr']/@value" />
        </param>
        <param name="szRFUserId">
          <xsl:value-of select="Field[@name = 'RFUserId']/@value" />
        </param>
      </params>
      <onError abort="yes">
      </onError>
    </callMethod>
  </xsl:template>
</xsl:stylesheet>