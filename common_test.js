/**
 * 一些通用方法
 */
(function(exports) {

    /**
     * 将string字符串转为html对象,默认创一个div填充
     * 因为很常用，所以单独提取出来了
     * @param {String} strHtml 目标字符串
     * @return {HTMLElement} 返回处理好后的html对象,如果字符串非法,返回null
     */
    exports.parseHtml = function(strHtml) {
        if (typeof strHtml !== 'string') {
            return strHtml;
        }
        // 创一个灵活的div
        var i,
            a = document.createElement('div');
        var b = document.createDocumentFragment();

        a.innerHTML = strHtml;

        while ((i = a.firstChild)) {
            b.appendChild(i);
        }

        return b;
    };

    /**
     * 将对象渲染到模板
     * @param {String} template 对应的目标
     * @param {Object} obj 目标对象
     * @return {String} 渲染后的模板
     */
    exports.renderTemplate = function(template, obj) {
        return template.replace(/[{]{2}([^}]+)[}]{2}/g, function($0, $1) {
            return obj[$1] || '';
        });
    };

    /**
     * 将对象渲染到模板
     * @param {String} template 对应的目标
     * @param {Object} obj 目标对象
     * @return {String} 渲染后的模板
     */
    exports.prepareData = function(template, obj) {
        return template.replace(/[{]{2}([^}]+)[}]{2}/g, function($0, $1) {
            return obj[$1] || '';
        });
    };

    /**
     * 定义一个计数器
     */
    var counterArr = [0];

    /**
     * 添加测试数据
     * @param {String} dom 目标dom
     * @param {Number} count 需要添加的数量
     * @param {Boolean} isReset 是否需要重置，下拉刷新的时候需要
     * @param {Number} index 属于哪一个刷新
     */
    exports.appendTestData = function(dom, count, isReset, index) {
      var testhtml = exports.testcomment(count, index);
      return exports.appendData(dom, isReset, testhtml)
    };

    /**
     *
     * @param {String} dom 目标dom
     * @param {Number} count 需要添加的数量
     * @param {Boolean} isReset 是否需要重置，下拉刷新的时候需要
     */
    exports.appendData = function(dom, isReset, data) {
        if (typeof dom === 'string') {
            dom = document.querySelector(dom);
        }

        if (isReset) {
            dom.innerHTML = '';
        }

        var template = '<div id=${commentlist.id} class="commentlist"> \
        <div id=${comment.id} class="comment"> \
          <div> \
            <div class="author"> \
              <div data-v-f3bf5228="" class="v-tooltip-container" style="z-index: 0;"> \
                <div class="v-tooltip-content"> \
                  <a href="javascript:void(0);" target="_blank" class="avatar"> \
                    <img src="{{commentAvatar}}"> \
                  </a> \
                </div> <!----> \
              </div> \
              <div class="info"> \
                <a href="javascript:void(0);" target="_blank" class="name">{{commentCommenter}} \
                </a> <!----> <!----> \
                <div class="meta"> \
                  <span>{{commentIndexAdd}}楼 · {{commentTime}} \
                  </span> \
                </div> \
              </div> \
            </div> \
            <div class="comment-wrap"> \
              <p>{{commentContent}}</p> \
              <span class="pull-right"> \
                <div class="tool-group" class="pull-right"> \
                  <a href="javascript:void(0)" onclick="addStar({{record.id}}, {{commentIndex + 1}})"> \
                    <span id="commentStar{{commentIndex}}" class="glyphicon glyphicon-star-empty" style="color:#969696;top:0px"></span> \
                  </a> \
                  <span id="commentStarNum{{commentIndex}}" class="label label-default">{{ commentStarNumber }}</span> \
                  <a ng-if="commentCommenter == user.username" class="commentTools comment-delete" href="javascript:void(0);" ng-click="deletefslcmt(record.id, commentId)"> \
                    <span>删除</span> \
                  </a> \
                  <a class="commentTools comment-reply" href="javascript:void(0);" ng-click="showSecondComment(comment, commentCommenter)"> \
                    <span>回复</span> \
                  </a> \
                </div> \
              </span> \
            </div> \
          </div> \
        </div>';

        var subCommentBegin = '<div class="sub-comment-list">';

        var subCommentTemplate = '<div id={{commentId}} class="sub-comment"> \
              <p> \
                <div data-v-f3bf5228="" class="v-tooltip-container" style="z-index: 0;"> \
                  <div class="v-tooltip-content"> \
                    <a href="javascript:void(0);" target="_blank">{{replier}} \
                    </a>： \
                  </div> <!----> \
                </div>  \
                <span> \
                  <a href="/u/0efd14e6d258" class="maleskine-author" target="_blank" data-user-slug="0efd14e6d258">@{{toCommenter}} \
                  </a> {{replyContent}} \
                </span> \
              </p> \
              <div class="sub-tool-group"> \
                <span>{{replyTime}} \
                </span> \
                <a class=""> \
                  <i class="iconfont ic-comment"> \
                  </i> \
                  <span>回复 \
                  </span> \
                </a> <!----> <!----> \
              </div> \
            </div>';

          var moreCommentTemplate = '<div class="sub-comment more-comment"> \
            <a class="add-comment-btn" href="javascript:void(0);" ng-click="showComment()"> \
              <span>查看全部{{commentChildCommentNumber}}条评论</span> \
              <span class="glyphicon glyphicon-list-alt"></span> \
            </a> <!----> <!----> <!----> \
          </div>';

          var commentEnd = '</div></div>';

        var html = '';

        for (var i = 0; i < data.length; i++) {
            html += exports.renderTemplate(template, {
                commentId: data[i].id,
                commentAvatar: data[i].avatar,
                commentCommenter: data[i].commenter,
                commentIndex: data[i].index,
                commentIndexAdd: data[i].index + 1,
                commentContent: data[i].content,
                commentStarNumber: data[i].starNumber,
                commentTime: data[i].time
            });
            if(data[i].childComment.length > 0){
              html += subCommentBegin;
              for (var j = 0; j < data[i].childComment.length; j++) {
                html += exports.renderTemplate(subCommentTemplate, {
                    commentId: data[i].childComment[j].id,
                    replier: data[i].childComment[j].replier,
                    toCommenter: data[i].childComment[j].toCommenter,
                    replyContent: data[i].childComment[j].replyContent,
                    replyTime: data[i].childComment[j].replyTime
                });
              }
            }
            if(data[i].childCommentLength > 3){
              html += exports.renderTemplate(moreCommentTemplate, {
                  commentChildCommentNumber: data[i].childCommentLength
              });
            }
            html += commentEnd;
        }

        var child = exports.parseHtml(html);

        dom.appendChild(child);
    };

    exports.testcomment = function(count, index){
      var datas=[],
          dateStr = (new Date()).toLocaleString();

      var counterIndex = index || 0;

      counterArr[counterIndex] = counterArr[counterIndex] || 0;

      for (var i = 0; i < count; i++) {
        var childs = [],
            data = {},
            child = {};
        data['id'] = counterArr[counterIndex];
        data['avatar'] = 'https://cdn2.jianshu.io/assets/default_avatar/10-e691107df16746d4a9f3fe9496fd1848.jpg';
        data['commenter'] = 'larry';
        data['index'] = counterArr[counterIndex];
        data['content'] = "This is a piece of comment";
        data['starNumber'] = counterArr[counterIndex];
        data['time'] = dateStr;
        for (var j = 0; j < (counterArr[counterIndex] > 3 ? 3 : i); j++){
          child['id'] = j;
          child['replier'] = 'klydream';
          child['toCommenter'] = 'larry';
          child['replyContent'] = "This is a piece of child comment";
          child['replyTime'] = dateStr;
          childs.push(child);
          //console.log(child)
        }
        data['childComment'] = childs;
        data['childCommentLength'] = counterArr[counterIndex];
        datas.push(data);
        counterArr[counterIndex]++;
      }
      //console.log(datas)
      return datas
    }

})(window.Common = {});
