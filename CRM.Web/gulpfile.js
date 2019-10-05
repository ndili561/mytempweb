/// <binding AfterBuild='Mini:PaceJs, Minify:Jquery-Validation-Unobtrusive' />
/*
 * This will script will pull the required librarys from node_modules folder
 * and put them in the wwwroot/etxlibs folder. SM
 */

var gulp = require('gulp');
var uglify = require('gulp-uglify');
var rename = require('gulp-rename');
var cleanCSS = require('gulp-clean-css');


var paths = {
    nodeModules: './node_modules/',
    dest: './wwwroot/extlibs/'
};

gulp.task('Copy:JQuery', () => {
    var filesToCopy = [
        `${paths.nodeModules}jquery/dist/jquery.js`,
        `${paths.nodeModules}jquery/dist/jquery.min.js`
    ];
    return gulp.src(filesToCopy)
        .pipe(gulp.dest(`${paths.dest}jquery/`));
});

gulp.task('Copy:Boostrap', () => {
    return gulp.src(`${paths.nodeModules}bootstrap/dist/**/*.*`)
        .pipe(gulp.dest(`${paths.dest}bootstrap/`));
});

//gulp.task('Copy:InspiniaJS', () => {
//    return gulp.src(`${paths.nodeModules}inspinia/dist/**/*.*`)
//        .pipe(gulp.dest(`${paths.dest}inspinia/`));

//    //return gulp.src(`${paths.dest}inspinia/js/inspinia.js`)
//    //    .pipe(uglify())
//    //    .pipe(rename({ suffix: '.min' }))
//    //    .pipe(gulp.dest(`${paths.dest}inspinia/js/`));
//});

//gulp.task('CopyMinify:InspiniaCss', () => {
//    return gulp.src(`${paths.inspinia}Content/style.css`)
//        .pipe(rename({ basename: 'inspinia' }))
//        .pipe(gulp.dest(`${paths.dest}inspinia/css/`))
//        .pipe(gulp.src(`${paths.dest}inspinia/css/*.css`))
//        .pipe(cleanCSS({ compatibility: 'ie8' }))
//        .pipe(rename({ suffix: ".min" }))
//        .pipe(gulp.dest(`${paths.dest}inspinia/css/`));
//});

gulp.task('Copy:Font-Awesome', () => {
    return gulp.src(`${paths.nodeModules}font-awesome/css/*.*`)
        .pipe(gulp.dest(`${paths.dest}font-awesome/css/`))
        .pipe(gulp.src(`${paths.nodeModules}font-awesome/fonts/*.*`))
        .pipe(gulp.dest(`${paths.dest}font-awesome/fonts/`));
});

gulp.task('Copy:AnimateCss', () => {
    return gulp.src(`${paths.nodeModules}animate.css/*.css`)
        .pipe(gulp.dest(`${paths.dest}animate.css/css/`));
});

gulp.task('Copy:ToastrJS', () => {
    var filesToCopy = [
        `${paths.nodeModules}toastr/toastr.js`,
        `${paths.nodeModules}toastr/build/toastr.min.js`
    ];
    gulp.src(filesToCopy)
        .pipe(gulp.dest(`${paths.dest}toastr/js/`));

    return gulp.src(`${paths.nodeModules}toastr/build/*.css`)
        .pipe(gulp.dest(`${paths.dest}toastr/css/`));
});

gulp.task('Copy:Jquery-Validation', () => {
    var filesToCopy = [
        `${paths.nodeModules}jquery-validation/dist/jquery.validate.js`,
        `${paths.nodeModules}jquery-validation/dist/jquery.validate.min.js`
    ];

    return gulp.src(filesToCopy)
        .pipe(gulp.dest(`${paths.dest}jquery-validation/`));
});

gulp.task('Copy:Jquery-Validation-Unobtrusive', () => {

    return gulp.src(`${paths.nodeModules}jquery-validation-unobtrusive/*.js`)
        .pipe(gulp.dest(`${paths.dest}jquery-validation-unobtrusive/`));
});

gulp.task('Minify:Jquery-Validation-Unobtrusive', () => {
    return gulp.src(`${paths.dest}jquery-validation-unobtrusive/jquery.validate.unobtrusive.js`)
        .pipe(uglify())
        .pipe(rename({ suffix: ".min" }))
        .pipe(gulp.dest(`${paths.dest}jquery-validation-unobtrusive/`));
});


gulp.task('Copy:MetisMenu',
    () => {
        gulp.src(`${paths.nodeModules}metismenu/dist/*.css`)
            .pipe(gulp.dest(`${paths.dest}metismenu/css/`));

        return gulp.src(`${paths.nodeModules}metismenu/dist/*.js`)
            .pipe(gulp.dest(`${paths.dest}metismenu/js/`));
    });

gulp.task('Copy:PaceJs', () => {

     gulp.src(`${paths.nodeModules}pace-js/*.js`)
        .pipe(gulp.dest(`${paths.dest}pacejs/js/`));

    return gulp.src(`${paths.nodeModules}pace-js/themes/green/pace-theme-loading-bar.css`)
            .pipe(gulp.dest(`${paths.dest}pacejs/css/`));
});

gulp.task('Mini:PaceJs',
    () => {
        return gulp.src(`${paths.dest}pacejs/css/pace-theme-loading-bar.css`)
            .pipe(cleanCSS({ compatibility: 'ie8' }))
            .pipe(rename({ suffix: ".min" }))
            .pipe(gulp.dest(`${paths.dest}pacejs/css/`));
    });