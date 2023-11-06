/**
 * @license Copyright (c) 2003-2023, CKSource Holding sp. z o.o. All rights reserved.
 * For licensing, see https://ckeditor.com/legal/ckeditor-oss-license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here. For example:
	 config.language = 'vi';
	// config.uiColor = '#AADC6E';
    //config.skin = "kama";
	config.height = 220;
	config.extraPlugins = 'youtube';
	config.youtube_width = '640';
    config.youtube_height = '480';
    config.youtube_responsive = true;

    config.youtube_related = true;

    config.youtube_older = false;
    config.youtube_controls = true;


};
