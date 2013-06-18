/// <reference path="nodelib/node.js" />
/// <reference path="node_modules/glob/glob.js " />
var sourceMatch = "../src/Nuget/*.nuspec";

if (!String.prototype.format) {
	String.prototype.format = function () {
		var args = arguments;
		return this.replace(/{(\d+)}/g, function (match, number) {
			return typeof args[number] != 'undefined'
			  ? args[number]
			  : match
			;
		});
	};
}
var asyncblock = require('asyncblock');

var currentDir = process.cwd();
asyncblock(function (flow) {

	var fs = require('fs');
	var path = require('path');

	var glob = require('glob');
	["*.nupkg", "**/build.*.nuspec"].forEach(function (pattern) {
		var files = glob.sync(pattern);
		files.forEach(function (file) {
			fs.unlink(file, function (err) {
				if (err) throw err;
			});
		});
	});
	var nuspecFiles = glob.sync(sourceMatch) || [];
	nuspecFiles.forEach(function (input) {
		var content = fs.readFileSync(input).toString();

		var versionRegex = /(<version>.*?\..*?)\.x\.x(<\/version>)/gi;
		//var dependencyRegex = /(<dependency id="GNU.*?)(" version="1.18)(.*?)(" \/>)/gi;
		//var nameRegex = /(<title>)(.*)(<\/title)/gi;
		//var idRegex = /(<id>)(.*)(<\/id>)/gi;
		var fileRegex = /(\<file src=\")/gi;
		var date = new Date();
		var idPrefix = "20";
		var namePrefix = " 2.0"
		var revision = (date.getUTCMonth() + 1) * 100 + date.getUTCDate();
		var build = date.getUTCHours() * 10000 + date.getUTCMinutes() * 100 + date.getUTCSeconds();
		content = content.replace(versionRegex, "$1.{0}.{1}$2".format(revision, build));
		//content = content.replace(dependencyRegex, "$1{0}$2.{1}.{2}$4".format(idPrefix, revision, build));
		//content = content.replace(nameRegex, "$1$2{0}$3".format(namePrefix));
		//content = content.replace(idRegex, "$1$2{0}$3".format(idPrefix));
		content = content.replace(fileRegex, "$1{0}\\".format(path.normalize(path.dirname(input))));
		var output = "build."+ path.basename(input), content;
		process.chdir(path.dirname(input));
		fs.writeFileSync(output, content);
		var spawn = require('child_process').spawn;
		//execute nuget  pack command 
		var pack = spawn(path.resolve(__dirname, 'nuget.exe'), ["pack", output, "-OutputDirectory", __dirname].concat(process.argv.slice(2)), {
			stdio: 'inherit',
			stderr: 'inherit',
		});
		pack.on("close", flow.add());
		flow.wait();
		var mv = require('mv');
		mv(output, path.resolve(__dirname, path.basename(input)), flow.add());
		flow.wait();
		process.chdir(currentDir);
	});

	typeof nupackDone == "function" && nupackDone();
});


