using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

// This is stub code for a theoretical feature to be added as a stretch goal.
// For the beta release, we will be using a subset of part information instead
// of the full citation.

public class PDFCitation {

	private FileStream pdf_file;

	private string part_name;

	private int page_number;

	private Texture2D page_texture;

	public bool initialized;

	public PDFCitation() {
		pdf_file = null;
		part_name = "";
		page_number = -1;
		page_texture = null;
	}

	public PDFCitation(string name, int page_num, FileStream raw_file) {
		pdf_file = raw_file;
		part_name = name;
		page_number = page_num;
		page_texture = makePageTexture();
	}

	private Texture2D makePageTexture() {
		// This is where you convert a PDF to texture.
		return new Texture2D(0, 0);
	}
}
