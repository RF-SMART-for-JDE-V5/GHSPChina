<?xml version="1.0" encoding="UTF-8"?>

<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="xml" encoding="UTF-8" omit-xml-declaration="no" indent="yes" />
  <xsl:template name="RFS_TxPSUpdateBack">
    <xsl:param name="SequenceNumber">
    </xsl:param>
    <Tx Sequence="{$SequenceNumber}" name="RFS_TxPSUpdate" Boundary="1">
      <Fields>
        <Field name="LabelName">
          <xsl:attribute name="value">
            <xsl:value-of select="params/param[@name = 'szLabelName']" />
          </xsl:attribute>
        </Field>
        <Field name="Mode">
          <xsl:attribute name="value">
            <xsl:value-of select="params/param[@name = 'cMode']" />
          </xsl:attribute>
        </Field>
        <Field name="ErrorCode">
          <xsl:attribute name="value">
            <xsl:value-of select="params/param[@name = 'cErrorCode']" />
          </xsl:attribute>
        </Field>
        <Field name="ErrorDescr">
          <xsl:attribute name="value">
            <xsl:value-of select="params/param[@name = 'szErrorDescr']" />
          </xsl:attribute>
        </Field>
        <Field name="RFUserId">
          <xsl:attribute name="value">
            <xsl:value-of select="params/param[@name = 'szRFUserId']" />
          </xsl:attribute>
        </Field>
      </Fields>
      <xsl:call-template name="Responses" />
    </Tx>
  </xsl:template>
</xsl:stylesheet>